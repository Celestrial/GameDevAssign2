using UnityEngine;
using System.Collections;

namespace comp476a2
{
    public class Node
    {
        public float key;//heap key
        public GameObject value;//heap value

        public Node(float key, GameObject value)
        {//constructor for node inner class
            this.key = key;
            this.value = value;
        }
        public bool lessThan(Node t)
        {//custom compare method
            return this.key < t.key;
        }

        // @Override
        public bool equals(Node obj)
        {
            if (this.value != obj.value)
            {
                return false;
            }
            return true;
        }

        //@Override
        //public String toString() {
        //  return "Node{" + "key=" + key + ", value=" + value + '}';
        //}
    }
}