import os
import sys
import hashlib
from datetime import datetime
from git import Repo, GitCommandError
from concurrent.futures import ThreadPoolExecutor, as_completed


# ------------------------------------------------------------
# Writes output variables for GitHub Actions workflow steps.
# If not running inside GitHub Actions, this does nothing.
# ------------------------------------------------------------
def set_action_output(name: str, value: str):
    github_output = os.getenv("GITHUB_OUTPUT")
    if github_output:
        with open(github_output, "a", encoding="utf-8") as f:
            f.write(f"{name}={value}\n")


# ------------------------------------------------------------
# Calculates MD5 hash of a file.
# Returns uppercase hash string.
# Returns "0" if file is missing, unreadable, or hidden.
# ------------------------------------------------------------
def calc_md5(file_path: str) -> str:
    if not os.path.exists(file_path) or os.path.basename(file_path).startswith("."):
        return "0"
    try:
        md5 = hashlib.md5()
        with open(file_path, "rb") as f:
            for chunk in iter(lambda: f.read(4096), b""):
                md5.update(chunk)
        return md5.hexdigest().upper()
    except Exception:
        return "0"


# ------------------------------------------------------------
# Processes a single .wpth file:
# - Calculates its MD5
# - Calculates companion .wptp MD5 if it exists and is not hidden
# - Builds raw GitHub URLs
# - Returns formatted database entry string
# ------------------------------------------------------------
def process_theme_file(targetfile: str, repo_root: str, owner: str, repo_name: str, ref: str) -> str:
    extension = ".wpth"
    targetpack = targetfile.replace(extension, ".wptp")

    # MD5 for the main .wpth file
    md5_file = calc_md5(targetfile)

    # Only include companion .wptp if it exists and is not hidden
    if os.path.exists(targetpack) and not os.path.basename(targetpack).startswith("."):
        md5_pack = calc_md5(targetpack)
        relative_pack = os.path.relpath(targetpack, repo_root).replace("\\", "/")
        url_pack = f"https://raw.githubusercontent.com/{owner}/{repo_name}/{ref}/{relative_pack}"
    else:
        md5_pack = "0"
        url_pack = ""

    # Repository-relative path and URL for the .wpth file
    relative_file = os.path.relpath(targetfile, repo_root).replace("\\", "/")
    url_file = f"https://raw.githubusercontent.com/{owner}/{repo_name}/{ref}/{relative_file}"

    # Build final database entry
    entry = f"{md5_file}|{md5_pack}|{url_file}"
    if url_pack:
        entry += f"|{url_pack}"

    return entry


# ------------------------------------------------------------
# Main execution entry point
# ------------------------------------------------------------
def main():
    if len(sys.argv) < 3:
        print("Usage: script.py <search_path> <output_file>")
        sys.exit(1)

    search_path = sys.argv[1]
    outputfile = sys.argv[2]
    extension = ".wpth"

    # Initialize Git repository
    repo = Repo(".", search_parent_directories=True)
    repo_root = repo.working_tree_dir

    # Determine GitHub repository owner and name
    repository_info = os.getenv("GITHUB_REPOSITORY")
    if repository_info:
        owner, repo_name = repository_info.split("/")
    else:
        # Fallback for local execution
        repo_name = os.path.basename(repo_root)
        owner = "unknown"

    # Actor who triggered workflow (if applicable)
    committer = os.getenv("GITHUB_ACTOR", "Unknown")

    # Use exact commit SHA for stable URLs
    ref = os.getenv("GITHUB_SHA") or repo.head.commit.hexsha

    print(f"Scanning directory: {search_path}")

    # Collect all non-hidden .wpth files and skip hidden directories
    theme_files = []
    for root, dirs, files in os.walk(search_path):
        # Skip hidden directories
        dirs[:] = [d for d in dirs if not d.startswith(".")]

        for file in files:
            # Skip hidden files
            if file.startswith("."):
                continue

            # Only include .wpth files
            if file.endswith(extension):
                full_path = os.path.join(root, file)

                # Skip if any parent directory is hidden
                relative_parts = os.path.relpath(full_path, search_path).split(os.sep)
                if any(part.startswith(".") for part in relative_parts[:-1]):
                    continue

                theme_files.append(full_path)

    print(f"Found {len(theme_files)} theme files. Calculating hashes in parallel...")

    # Parallel processing using ThreadPoolExecutor for I/O-bound MD5 hashing
    paths = []
    with ThreadPoolExecutor() as executor:
        futures = [
            executor.submit(process_theme_file, file, repo_root, owner, repo_name, ref)
            for file in theme_files
        ]

        # Collect results as they complete
        for future in as_completed(futures):
            paths.append(future.result())

    # Sort results for deterministic output
    paths.sort()
    path_count = len(paths)

    # Set GitHub Actions outputs
    set_action_output("path_count", str(path_count))
    set_action_output("paths", " ".join(paths))

    print(f"Processed {path_count} files successfully.")

    new_content = "\n".join(paths)

    # Avoid unnecessary commits if nothing changed
    old_content = ""
    if os.path.exists(outputfile):
        with open(outputfile, "r", encoding="utf-8") as f:
            old_content = f.read()

    if new_content != old_content:
        # Write updated database
        with open(outputfile, "w", encoding="utf-8") as f:
            f.write(new_content)

        # Stage and commit changes
        repo.index.add([outputfile])
        repo.index.commit(
            f"{committer} updated themes database on {datetime.utcnow().strftime('%d/%m/%Y %H:%M:%S')} UTC"
        )

        try:
            # Push to the current branch
            repo.git.push("origin", repo.active_branch.name)
            print("Changes committed and pushed.")
        except GitCommandError as e:
            print(f"Push failed: {e}")
    else:
        print("No changes detected. Skipping commit.")

    sys.exit(0)


if __name__ == "__main__":
    main()