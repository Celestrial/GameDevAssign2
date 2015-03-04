using UnityEngine;
using System.Collections;

namespace comp476a2
{
    public class FlexHeap {

      public BinTree myMinTree;//Underlying binary tree object
      public static bool minHeap;//flag for min or max heap designation
      public static int lastAdded;//index of last added node in heap
      public static int nextNode;//index of next node to be added

      public class BinTree
      {

          public Node[] array;//array of nodes which store keys and values
          public int DEFAULT_SIZE = 10;

          public BinTree()
          {
              array = new Node[DEFAULT_SIZE];
              nextNode = 1;
          }

          public int size()
          {
              return lastAdded;
          }//return number of values in tree

          public bool isEmpty()
          {
              return lastAdded == 0;
          }//boolean if tree is empty

          public Node root()
          {
              if (array[1] == null)
              {
                  return null;
              }
              else
              {
                  return array[1];
              }
          }//returns root node

          public int parent(int position)
          {
              if (position == 1)
              {
                  return 0;
              }
              else
              {
                  Node tempNode;
                  if ((position % 2) == 0)
                  {
                      return position / 2;
                  }
                  else
                  {
                      return (position - 1) / 2;
                  }
              }
          }//return index of parent of sent index

          public bool isInternal(int position)
          {
              if (((position * 2) > array.Length) || (position * 2 > lastAdded))
              {
                  return false;
              }
              else
              {
                  return true;
              }
          }//return bool if index internal

          public bool isExternal(int position)
          {
              return !isInternal(position);
          }//return bool if index external

          public bool isRoot(int position)
          {
              return position == 1;
          }//return bool if index is root

          public Node replace(int position, Node element)
          {
              Node tempNode = new Node(array[position].key, array[position].value);
              array[position] = element;
              return tempNode;
          }//replace node at given index with passed node

          public Node left(int position)
          {
              if (array[position * 2] == null)
              {
                  return null;
              }
              else
              {
                  return array[position * 2];
              }
          }//returns left node of passed index

          public Node right(int position)
          {
              if (array[position * 2 + 1] == null)
              {
                  //System.out.println("Error!1 No right child.");
                  return null;
              }
              else
              {
                  /*Node tempNode = new Node(array[position*2+1].key, array[position*2+1].value);
                   return tempNode;*/
                  return array[position * 2 + 1];
              }
          }//returns right node of passed index

          public bool hasLeft(int position)
          {
              return array[position * 2] != null;
          }//returns bool if passed index has left node

          public bool hasRight(int position)
          {
              return array[position * 2 + 1] != null;
          }//returns bool if passed index has right node

          //@Override
          //public String toString() {
          //  int n = 1;
          //  String tempString = "";
          //  int k = 0;
          //  int r;
          //  int j = 1;
          //  while (k < lastAdded) {
          //    tempString += "|";
          //    for (int i = 0; i < n && (k < lastAdded); ++i) {// number of tabs to print
          //      //System.out.print("("+array[k+1].key+","+array[k+1].value+")"+"\t");
          //      tempString += "(" + array[k + 1].key + "," + array[k + 1].value + ")"/*+"\t"*/;
          //      if ((k + 1) % 2 == 1) {
          //        tempString += "|";
          //      }
          //      ++k;
          //    }
          //    n *= 2;
          //    //System.out.println();
          //    tempString += "\n";
          //  }
          //  return tempString;
          //}
      }

      public FlexHeap() {
        FlexHeap.nextNode = 1;
        minHeap = true;
        lastAdded = 0;
        myMinTree = new BinTree();
      }

      private void expand() {
        Node[] tempArray = new Node[myMinTree.array.Length * 2];
        for (int i = 0; i <= lastAdded; ++i) {
          tempArray[i] = myMinTree.array[i];
        }
        myMinTree.array = tempArray;
      }//double size of underlying array

      public void insert(float key, GameObject value) {
        Node newNode = new Node(key, value);
        myMinTree.array[nextNode] = newNode;
        ++nextNode;
        ++lastAdded;
        inRepair(lastAdded);
        if (((double) lastAdded / (double) myMinTree.array.Length) > 0.89) {
          expand();
        }
      }//insert element in to heap and maintain integrety

      private void inRepair(int last) {
        if (myMinTree.parent(last) != 0) {
          if (minHeap) {
            if (myMinTree.array[last].lessThan(myMinTree.array[myMinTree.parent(last)])) {
              myMinTree.array[last] =
                      myMinTree.replace(myMinTree.parent(last), myMinTree.array[last]);
              inRepair(myMinTree.parent(last));
            }
          } else {
            if (myMinTree.array[myMinTree.parent(last)].lessThan(myMinTree.array[last])) {
              myMinTree.array[last] =
                      myMinTree.replace(myMinTree.parent(last), myMinTree.array[last]);
              inRepair(myMinTree.parent(last));
            }
          }
        }
      }//repair method to maintain integrety when inserting element into heap

      public Node remove() {
        if (myMinTree.isEmpty()) {
          return null;
        }
        Node tempNode = new Node(myMinTree.array[1].key, myMinTree.array[1].value);
        myMinTree.array[1] = myMinTree.array[lastAdded];
        myMinTree.array[lastAdded--] = null;
        --nextNode;
        repairOut(1);
        return tempNode;
      }//remove element from heap and maintain integrety

      private void repairOut(int rootNode) {
        if (myMinTree.isExternal(rootNode)) {
          return;
        }
        if (minHeap) {
          if (myMinTree.right(rootNode) != null) {
            if (myMinTree.left(rootNode).lessThan(myMinTree.right(rootNode))
                    && myMinTree.left(rootNode).lessThan(myMinTree.array[rootNode])) {
              Node tempNode = myMinTree.array[rootNode];
              myMinTree.array[rootNode] = myMinTree.array[rootNode * 2];
              myMinTree.array[rootNode * 2] = tempNode;
              repairOut(rootNode * 2);
            } else if (!myMinTree.left(rootNode).lessThan(myMinTree.right(rootNode))
                    && myMinTree.right(rootNode).lessThan(myMinTree.array[rootNode])) {
              Node tempNode = myMinTree.array[rootNode];
              myMinTree.array[rootNode] = myMinTree.array[rootNode * 2 + 1];
              myMinTree.array[rootNode * 2 + 1] = tempNode;
              repairOut(rootNode * 2 + 1);
            }
          } else {
            if (myMinTree.left(rootNode).lessThan(myMinTree.array[rootNode])) {
              Node tempNode = myMinTree.array[rootNode];
              myMinTree.array[rootNode] = myMinTree.array[rootNode * 2];
              myMinTree.array[rootNode * 2] = tempNode;
              repairOut(rootNode * 2);
            }
          }
        } else {
          if (myMinTree.right(rootNode) != null) {
            if (!myMinTree.left(rootNode).lessThan(myMinTree.right(rootNode))
                    && !myMinTree.left(rootNode).lessThan(myMinTree.array[rootNode])) {
              Node tempNode = myMinTree.array[rootNode];
              myMinTree.array[rootNode] = myMinTree.array[rootNode * 2];
              myMinTree.array[rootNode * 2] = tempNode;
              repairOut(rootNode * 2);
            } else if (myMinTree.left(rootNode).lessThan(myMinTree.right(rootNode))
                    && !myMinTree.right(rootNode).lessThan(myMinTree.array[rootNode])) {
              Node tempNode = myMinTree.array[rootNode];
              myMinTree.array[rootNode] = myMinTree.array[rootNode * 2 + 1];
              myMinTree.array[rootNode * 2 + 1] = tempNode;
              repairOut(rootNode * 2 + 1);
            }
          } else {
            if (!myMinTree.left(rootNode).lessThan(myMinTree.array[rootNode])) {
              Node tempNode = myMinTree.array[rootNode];
              myMinTree.array[rootNode] = myMinTree.array[rootNode * 2];
              myMinTree.array[rootNode * 2] = tempNode;
              repairOut(rootNode * 2);
            }
          }
        }
      }//repair method to maintain integrety when remove element from heap

      public void toggleHeap() {
        minHeap = !minHeap;
        bottomUp();
      }//switch heap between min/max using bottom up construction

      public void switchMinHeap() {
        if (!minHeap) {
          toggleHeap();
        }
      }//if heap is max, switch to min

      public void switchMaxHeap() {
        if (minHeap) {
          toggleHeap();
        }
      }//if heap is min, switch to max

      private void bottomUp() {
        int k = 1;
        while (k < lastAdded) {
          k *= 2;
        }
        k /= 2;
        Node[] newArray = new Node[myMinTree.array.Length];
        for (int i = lastAdded; i > k; --i) {
          newArray[i] = myMinTree.array[i];
        }
        for (int i = k; i != 0; --i) {
          newArray[i] = myMinTree.array[i];
          repairOut(i);
        }
      }//rebuild heap from bottom up with given number of elements

      public int count()
      {
          return nextNode - 1;
      }

      ////@Override
      //public string toString() {
      //  return myMinTree.toString();
      //}

      public BinTree getMyMinTree() {//FOR TESTING ONLY
        return myMinTree;
      }//for debugging only!!!
    }
}