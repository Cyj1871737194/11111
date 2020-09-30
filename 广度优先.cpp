#include <iostream>
#include <string>
#include <cstring>
#include <cmath>
#include <vector>
#include <queue>
#include <set>
using namespace std;
#define N 9
int jc[N+1]={1,1,2,6,24,120,720,5040,40320,362880};
typedef struct data
{
    int arr[N];
    int hash;
    int pos;
    int step;
}Node;
 
int dir[4][2]={
    {0,1},
    {1,0},
    {0,-1},
    {-1,0}
};
int cantor(int arr[N])
{
    int i,j;
    int sum=0;
    for( i=0;i<N;i++)
    {
        int nmin=0;
        for(j=i+1;j<N;j++)
        {
            if(arr[i]>arr[j])
                nmin++;
        }
        sum+=(nmin*jc[N-i-1]);
 
    }
 
    return sum;
}
void swap(int *arr,int i,int j)
{
    int t=arr[i];
    arr[i]=arr[j];
    arr[j]=t;
}
void printArray(int * arr)
{
    int i,j;
    for (i=0;i<N;i++)
    {
        if(i%3==0)
            cout<<"\n";
        cout<<arr[i]<<" ";
    }
    cout<<endl;
}
void copyArray(int src[N],int target[N])
{
    int i;
    for(i=0;i<N;i++)
    {
        target[i]=src[i];
    }
 
}
int bfs(int arr[N],int sHash,int tHash)
{
    if(sHash==tHash)
        return 0;
    int i,j;
    queue<Node> q;
    set<int> setHash;
    Node now,next;
    copyArray(arr,now.arr);
    int pos=0;
    for (i=0;i<N;i++)
    {
        if(arr[i]==0)
            break;
        pos++;
    }
    now.hash=sHash;
    now.step=0;
    next.step=0;
    now.pos=pos;
    q.push(now);
    setHash.insert(now.hash);
 
    while(!q.empty())
    {
        now=q.front();
        q.pop();
        for (i=0;i<4;i++)
        {
            int offsetX=0,offsetY=0;
            offsetX=(now.pos%3+dir[i][0]);
            offsetY=(now.pos/3+dir[i][1]);
 
            if(offsetX>=0&&offsetX<3&&offsetY<3&&offsetY>=0)
            {
                copyArray(now.arr,next.arr);
                next.step=now.step;
 
                next.step++;
                swap(next.arr,now.pos,offsetY*3+offsetX);
 
                next.hash=cantor(next.arr);
                next.pos=(offsetY*3+offsetX);
                int begin=setHash.size();
                setHash.insert(next.hash);
                int end=setHash.size();
 
                if(next.hash==tHash){
 
                    return next.step;
                }
 
                if(end>begin)
                {
 
                    q.push(next);
                }
 
            }
        }
 
    }
    return -1;
}
int inversion(int arr[N])
{
    int sum=0;
    for(int i=0;i<N;++i)
    {
        for(int j=i+1;j<N;++j)
        {
            if(arr[j]<arr[i]&&arr[j]!=0)
                sum++;
            }
        }
    }
 
    return sum;
 
}
int main(int argc, char **argv)
{
    int i,j;
    string s="123456780";
    string t="123456078";
    int is[N],it[N];
    for (i=0;i<9;i++)
    {
        if (s.at(i)>='0'&&s.at(i)<='8')
        {
            is[i]=s.at(i)-'0';
        }else
        {
            is[i]=0;
        }
        if (t.at(i)>='0'&&t.at(i)<='8')
        {
            it[i]=t.at(i)-'0';
        }else
        {
            it[i]=0;
        }
    }
    int sHash,tHash;
 
    sHash=cantor(is);
    tHash=cantor(it);
    int inver1=inversion(is);
    int inver2=inversion(it);
    if((inver1+inver2)%2==0)
    {
        int step=bfs(is,sHash,tHash);
        cout<<step<<endl;
    }
    else
    {
        cout<<"无法从初始状态到达目标状态"<<endl;
    }
 
    return 0;
}
