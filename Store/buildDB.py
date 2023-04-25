import os
import sys
from datetime import datetime
from git.repo import Repo
import git
import hashlib

def set_action_output(name: str, value: str):
    with open(os.environ["GITHUB_OUTPUT"], "a") as myfile:
        myfile.write(f"{name}={value}\n")

def CalcMD5(file):
    try:
        if os.path.exists(file):
            md5_hash = hashlib.md5()
            with open(file,"rb") as f:
                for byte_block in iter(lambda: f.read(4096),b""):
                    md5_hash.update(byte_block)
            return str(md5_hash.hexdigest()).upper()
        else:
            return '0'
    except:
        return '0'
    
def main():
    path = sys.argv[1]
    extension = sys.argv[2]
    outputfile = sys.argv[3]
    repo = git.Repo('.', search_parent_directories=True)
    repo.working_tree_dir
    
    print('Searching inside directory: ' + path)
    print('for files with extention: ' + extension)

    path_count = 0
    paths = ''
    for root, dirs, files in os.walk(path):
        for file in files:
            if file.endswith(f'{extension}'):
                targetfile = root + '/' + str(file)
                targetpack = root + '/' + str(file).replace('.wpth', '.wptp')
                md5_hash_file_result = CalcMD5(targetfile)    
                md5_hash_pack_result = CalcMD5(targetpack)    
                url_file = 'https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/master/' + targetfile + '?raw=true'
                url_pack = 'https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/master/' + targetpack + '?raw=true'

            paths = paths + md5_hash_file_result + '|' + md5_hash_pack_result + '|' +  url_file + '|' + url_pack + '\n'
            path_count = path_count + 1

    set_action_output('path_count', path_count)
    set_action_output('paths', paths.replace('\n', ' '))
    
    print('Found ' + str(path_count) + ' files: ')
    print(paths)

    f = open("" + outputfile + "", "w")
    f.write(paths)
    f.close()

    repo.index.add(['' + outputfile + ''])
    repo.index.commit('Themes database is modified on ' + datetime.now().strftime("%d/%m/%Y %H:%M:%S") + ' GMT+00:00')
    origin = repo.remotes[0]
    origin.push()

    sys.exit(0)


if __name__ == "__main__":
    main()