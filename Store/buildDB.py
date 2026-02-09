import os
import sys
from datetime import datetime
from git import Repo
import hashlib

# Function to set GitHub Actions outputs
def set_action_output(name: str, value: str):
    github_output = os.getenv("GITHUB_OUTPUT")
    if github_output:
        with open(github_output, "a") as f:
            f.write(f"{name}={value}\n")

# Calculate MD5 hash of a file
def calc_md5(file_path: str) -> str:
    try:
        if not os.path.exists(file_path) or os.path.basename(file_path).startswith("."):
            return "0"
        md5 = hashlib.md5()
        with open(file_path, "rb") as f:
            for chunk in iter(lambda: f.read(4096), b""):
                md5.update(chunk)
        return md5.hexdigest().upper()
    except Exception:
        return "0"

def main():
    if len(sys.argv) < 3:
        print("Usage: script.py <search_path> <output_file>")
        sys.exit(1)

    path = sys.argv[1]
    outputfile = sys.argv[2]
    extension = ".wpth"

    repository_info = os.getenv("GITHUB_REPOSITORY")
    owner, repo_name = repository_info.split("/") if repository_info else ("unknown", "unknown")
    committer = os.getenv("GITHUB_ACTOR", "Unknown")

    repo = Repo('.', search_parent_directories=True)

    print(f"Searching inside directory: {path} for {extension} files")

    paths = []
    path_count = 0

    for root, dirs, files in os.walk(path):
        # Skip hidden directories
        dirs[:] = [d for d in dirs if not d.startswith(".")]

        for file in files:
            # Skip hidden files
            if file.startswith("."):
                continue

            if file.endswith(extension):
                full_path = os.path.join(root, file)

                # Skip if any parent folder is hidden
                relative_parts = os.path.relpath(full_path, path).split(os.sep)
                if any(part.startswith(".") for part in relative_parts[:-1]):
                    continue

                targetfile = full_path
                targetpack = os.path.join(root, file.replace(extension, ".wptp"))

                md5_file = calc_md5(targetfile)
                md5_pack = calc_md5(targetpack) if os.path.exists(targetpack) else "0"

                url_file = f'https://github.com/{owner}/{repo_name}/blob/main/{targetfile}?raw=true'
                
                # Only include URL if .wptp exists and is not in a hidden directory
                include_url_pack = os.path.exists(targetpack) and not any(part.startswith(".") for part in os.path.relpath(targetpack, path).split(os.sep)[:-1])
                url_pack = f'https://github.com/{owner}/{repo_name}/blob/main/{targetpack}?raw=true' if include_url_pack else ""
                
                entry = f"{md5_file}|{md5_pack}|{url_file}"
                if url_pack:
                    entry += f"|{url_pack}"

                paths.append(entry)
                path_count += 1

    set_action_output("path_count", str(path_count))
    set_action_output("paths", " ".join(paths))

    print(f"Found {path_count} files:")
    print("\n".join(paths))

    with open(outputfile, "w") as f:
        f.write("\n".join(paths))

    # Commit and push
    repo.index.add([outputfile])
    repo.index.commit(f'{committer} updated themes database on {datetime.utcnow().strftime("%d/%m/%Y %H:%M:%S")} UTC')
    repo.remotes[0].push()

if __name__ == "__main__":
    main()