using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//We make it generic so we can make heaps of other types too.
public class Heap<T> where T : IHeapItem<T>
{
    //We can now specify an array of T which represents the type declared by the user.
    T[] items;
    //The amount of items currently in the heap.
    int currentItemCount;

    //Constructor that takes a max heap size. Because it's difficult to resize arrays we want to know the size beforehand.
    public Heap(int maxHeapSize)
    {
        //initialize the array with the max heapsize.
        items = new T[maxHeapSize];
    }

    //Method for adding items to our heap (array);
    public void Add(T item)
    {
        //We set the index of the added item to the currentItemCount add it at the end of the array
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        //Now we sort it up to it's correct position by using our sortUp method.
        SortUp(item);
        //We need to increment the currentItemCount by 1 because we added a new item.
        currentItemCount++;
    }

    //Function that removes the first item of the heap.
    public T RemoveFirst()
    {
        //Store the first item in the heap.
        T firstItem = items[0];
        //We are about to remove an item so we substract one of the itemcount.
        currentItemCount--;
        //Now we take the last item of our heap and put it in to the first place.
        items[0] = items[currentItemCount];
        //Change the heapindex of the item we brought to the first place.
        items[0].HeapIndex = 0;
        //Now we want to sort that item down to move to the correct place and rebuild the heap.
        SortDown(items[0]);

        return firstItem;
    }

    //We want to check if the heap contains a specific item. We do this by checking the incoming item with the item in the heap.
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    //Sometimes we find a new path to node hat already has been found a path to. We want to update this node. Therefore we create this function.
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    //Returns the current amount of items in the heap.
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    //Sort our item down to get in to the correct place of the heap.
    void SortDown(T item)
    {
        //We enter a loop.
        while (true)
        {
            //First we want to know the indexes of our childs.
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            //We want to check if we have a child on the left.
            if (childIndexLeft < currentItemCount)
            {
                //If so we change our swapindex to the left child.
                swapIndex = childIndexLeft;

                //We also want to check if we have a child on the right. If we do we need to start comparing the left and the right child and determin which one has a higher priority.
                //Now we know which one of our childs has the highest priorty.
                if (childIndexRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                //Now we want to compare the priority of our child with the priority of our parent. If our highest priority child has a higher priority than our
                //parent we want to swap them.
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    //if the parent has a higher priority than it's children we dont have to swap.
                    return;
                }
            }
            else
            {
                //If the item doesnt have any children we can return.
                return;
            }
        }
    }

    void SortUp(T item)
    {
        //First we create an integer that holds the index of the parent item.
        int parentIndex = (item.HeapIndex - 1) / 2;

        //We enter a loop
        while (true)
        {
            //We create a temporary T to store the parent item in.
            T parentItem = items[parentIndex];
            //Comparing results in -1, 0 or 1. High priority is 1. Same priority is 0. Less priority is -1. So if the parent has a higher priority (lower fcost)
            //we should Swap our two items.
            if (item.CompareTo(parentItem) > 0)
            {
                //If we have a high priorty we swap.
                Swap(item, parentItem);
            }
            else
            {
                //Else we can stop and we have nothing more to swap.
                break;
            }

            //Update the parent after swapping the item.
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    //Swap 2 items in our heap and change their indexes.
    void Swap(T itemA, T itemB)
    {
        //First swap the actual items in the array.
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        //We need to create a temporary int so we can store the index of item A
        int itemAIndex = itemA.HeapIndex;
        //After that we change the index of item A to the index of item B
        itemA.HeapIndex = itemB.HeapIndex;
        //At last we change the index of item B to our stored index of item A 
        itemB.HeapIndex = itemAIndex;
    }
}

//We the items in our heap to keep track of their own heapindex. Because we have a generic class it doesn't know it's capable of all this.
//We need to make an interface so we can add the functionality to our class.
//In this interface we also implement another interface wich allows us to compare items.
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
