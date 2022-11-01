import numpy as np
import sys

def ConvertWrapper(soup, width, height):

    soupFormatted = np.array([[int(x) for x in y.split(',')] for y in soup.splitlines()])
    
    soupWrapped = np.pad(soupFormatted, 1, mode='wrap')

    rows = len(soupWrapped)    
    cols = len(soupWrapped[0])

    print(rows)
    print(cols)

    for row in soupWrapped:
        #print(row)
        for item in row:
            print(item)

if __name__ == "__main__":
     ConvertWrapper(sys.argv[1], sys.argv[2], sys.argv[3])