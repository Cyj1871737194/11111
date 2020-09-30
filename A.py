import numpy as np
import operator
O=int(input(("请输入方阵的行/列数：")))
A=list(input("请输入初始状态："))
B=list(input("请输入目标状态："))
z=0
M=np.zeros((O,O))
N=np.zeros((O,O))
for i in range(O):
    for j in range(O):
        M[i][j]=A[z]
        N[i][j]=B[z]
        z=z+1
openlist=[]

class State:
    def __init__(self,m):
        self.node=m
        self.f=0#f(n)=g(n)+h(n)
        self.g=0#g(n)
        self.h=0#h(n)
        self.father=None

init = State(M)
goal=State(N)
def h(s):
    a=0
    for i in range(len(s.node)):
        for j in range(len(s.node[i])):
            if s.node[i][j]!=goal.node[i][j]:
                a=a+1
    return a
def list_sort(l):
    cmp=operator.attrgetter('f')
    l.sort(key=cmp)
 
def A_star(s):
    global openlist
    openlist=[s]
    while(openlist):
        get=openlist[0]   
        if (get.node==goal.node).all():
            return get
        openlist.remove(get)
        for a in range(len(get.node)):
            for b in range(len(get.node[a])):
                if get.node[a][b]==0:
                    break
            if get.node[a][b]==0:
                break
        for i in range(len(get.node)):
            for j in range(len(get.node[i])):
                c=get.node.copy()
                if (i+j-a-b)**2==1:
                    c[a][b]=c[i][j]
                    c[i][j]=0
                    new=State(c)
                    new.father=get
                    new.g=get.g+1
                    new.h=h(new)
                    new.f=new.g+new.h
                    openlist.append(new)
        list_sort(openlist)
def printpath(f):
    if f is None:
        return
    printpath(f.father)
    print(f.node)
   


final=A_star(init)
if final:
    print("有解，解为：")
    printpath(final)
else:
    print("无解")