import os,time
import binascii
from Crypto.Cipher import AES
from Crypto.Random import get_random_bytes
from Crypto.Util.Padding import pad,unpad


def byte2hex(str):return bytes.fromhex(str)
def unpd(str):
    try:
        text=unpad(str,16)
    except ValueError:return False
    print(text)
    if text[2]=="H":    #flagがHackTo{}となることを利用し、text[2]=="H"となる場合終了になります
        print("\n\nPerfect!!!")
        print(text)
        return True
    return False


cryptkey=byte2hex("4861 636b 546f 7b34 3335 2841 4553 295f") #16byteの鍵

decoded=byte2hex("683d680adca29d5aa59d8c178ed343c27778915024d8762af022031d7d49296914396abb3f514de6c3ec35a70b8bebdf")
#=b'h=h\n\xdc\xa2\x9dZ\xa5\x9d\x8c\x17\x8e\xd3C\xc2wx\x91P$\xd8v*\xf0"\x03\x1d}I)i\x149j\xbb?QM\xe6\xc3\xec5\xa7\x0b\x8b\xeb\xdf'
print(decoded)



def checkfun(number):
    may_iv=number
    print(may_iv)
    byted=may_iv.to_bytes(16,'big')
    print(byted)
    try:
        aes=AES.new(key=cryptkey,mode=AES.MODE_CBC,iv=byted)
    except ValueError:return False
    if unpd(aes.decrypt(decoded))==True:
        return True
    return False


check=False
i=31

may_iv=0
t1=time.time()
while check==False: #神の信託を聞かずに（？）力で突破します
    if checkfun(i)==True:
        print("Found!")
        print(hex(i))
        check=True
    else:i+=1

t2=time.time()
time=t2-t1
print(f"時間:{time}")






