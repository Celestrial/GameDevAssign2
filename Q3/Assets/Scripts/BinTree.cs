//using UnityEngine;
//using System.Collections;

//namespace comp476a2
//{
//    public class BinTree
//    {

//        private Node[] array;//array of nodes which store keys and values
//        private int DEFAULT_SIZE = 10;

//        private BinTree()
//        {
//            array = new Node[DEFAULT_SIZE];
//            nextNode = 1;
//        }

//        public int size()
//        {
//            return lastAdded;
//        }//return number of values in tree

//        public bool isEmpty()
//        {
//            return lastAdded == 0;
//        }//boolean if tree is empty

//        public Node root()
//        {
//            if (array[1] == null)
//            {
//                return null;
//            }
//            else
//            {
//                return array[1];
//            }
//        }//returns root node

//        public int parent(int position)
//        {
//            if (position == 1)
//            {
//                return 0;
//            }
//            else
//            {
//                Node tempNode;
//                if ((position % 2) == 0)
//                {
//                    return position / 2;
//                }
//                else
//                {
//                    return (position - 1) / 2;
//                }
//            }
//        }//return index of parent of sent index

//        public bool isInternal(int position)
//        {
//            if (((position * 2) > array.length) || (position * 2 > lastAdded))
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }//return bool if index internal

//        public bool isExternal(int position)
//        {
//            return !isInternal(position);
//        }//return bool if index external

//        public bool isRoot(int position)
//        {
//            return position == 1;
//        }//return bool if index is root

//        public Node replace(int position, Node element)
//        {
//            Node tempNode = new Node(array[position].key, array[position].value);
//            array[position] = element;
//            return tempNode;
//        }//replace node at given index with passed node

//        public Node left(int position)
//        {
//            if (array[position * 2] == null)
//            {
//                return null;
//            }
//            else
//            {
//                return array[position * 2];
//            }
//        }//returns left node of passed index

//        public Node right(int position)
//        {
//            if (array[position * 2 + 1] == null)
//            {
//                //System.out.println("Error!1 No right child.");
//                return null;
//            }
//            else
//            {
//                /*Node tempNode = new Node(array[position*2+1].key, array[position*2+1].value);
//                 return tempNode;*/
//                return array[position * 2 + 1];
//            }
//        }//returns right node of passed index

//        public bool hasLeft(int position)
//        {
//            return array[position * 2] != null;
//        }//returns bool if passed index has left node

//        public bool hasRight(int position)
//        {
//            return array[position * 2 + 1] != null;
//        }//returns bool if passed index has right node

//        //@Override
//        //public String toString() {
//        //  int n = 1;
//        //  String tempString = "";
//        //  int k = 0;
//        //  int r;
//        //  int j = 1;
//        //  while (k < lastAdded) {
//        //    tempString += "|";
//        //    for (int i = 0; i < n && (k < lastAdded); ++i) {// number of tabs to print
//        //      //System.out.print("("+array[k+1].key+","+array[k+1].value+")"+"\t");
//        //      tempString += "(" + array[k + 1].key + "," + array[k + 1].value + ")"/*+"\t"*/;
//        //      if ((k + 1) % 2 == 1) {
//        //        tempString += "|";
//        //      }
//        //      ++k;
//        //    }
//        //    n *= 2;
//        //    //System.out.println();
//        //    tempString += "\n";
//        //  }
//        //  return tempString;
//        //}
//    }
//}