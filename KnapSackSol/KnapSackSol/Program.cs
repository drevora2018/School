using System;
using System.Collections.Generic;
using System.Diagnostics;

class Node
{
    public int Collectivebenefit;
    public int CollectiveWeight;
    public int[] BinaryRepresentation;
    public int index;
    public List<Item> Items = new List<Item>();

    public Node(int[] Binary, List<Item> items, int i)
    {
        BinaryRepresentation = Binary;
        PlaceItemsInList(items);
        CalculateBen();
        index = i;
    }

    public void PlaceItemsInList(List<Item> items)
    {
        for (int i = 0; i < BinaryRepresentation.Length; i++)
        {
            if (BinaryRepresentation[i] == 1)
            {
                Items.Add(items.Find(x => x.ID == i));
            }
            
        }
    }
    public void CalculateBen()
    {
        if (Items.Count <= 0) {  return; }
        foreach (var item in Items)
        {
            if (item == null)
            {
                Collectivebenefit = 0; CollectiveWeight = 0;
            }
            else
            {
                Collectivebenefit += item.Benefit;
                CollectiveWeight += item.Weight;
            }
            
        }
    }
}

class Item
{
    public int ID;
    public int Weight;
    public int Benefit;

    public Item(int id, int wei, int ben)
    {
        ID = id;
        Weight = wei;
        Benefit = ben;
    }
}

class KnapsackBFS
{
    public List<Item> Items = new List<Item> ();
    int[] Bin = new int[11]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public Node bestNode = null;
    
    public KnapsackBFS(string path)
    {
        PlaceItems(path);
    }
    public void PlaceItems(string path)
    {
        StreamReader sr = new StreamReader(path);
        while(!sr.EndOfStream)
        {
            var line = sr.ReadLine().Split(" ");
            Items.Add(new Item(Int32.Parse(line[0]), Int32.Parse(line[2]), Int32.Parse(line[1])));
        }
    }

    
    public void GenerateTreeBFS()
    {
        bestNode = null;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(new Node(Bin, Items, 0));
        bestNode = queue.Peek();


        while (queue.Count > 0)
        {
            Node node = queue.Dequeue();
            if (node.index == 11)
            {
                break;
            }
            

            int[] leftValue = new int[11];
            node.BinaryRepresentation.CopyTo(leftValue, 0);
            leftValue[node.index] = 1;

            Node leftChild = new Node(leftValue, Items, node.index + 1);
            if (leftChild.CollectiveWeight < 420)
            {
                queue.Enqueue(leftChild);
                if (leftChild.Collectivebenefit >= bestNode.Collectivebenefit)
                {
                    bestNode = leftChild;
                }
            }
            int[] rightValue = new int[11];
            node.BinaryRepresentation.CopyTo(rightValue, 0);
            Node rightChild = new Node(rightValue, Items, node.index + 1);
            queue.Enqueue(rightChild);
        }

    }
    public void GenerateTreeDFS()
    {
        
        bestNode = null;
        Stack<Node> stack = new Stack<Node>();

        stack.Push(new Node(Bin, Items, 0));
        bestNode = stack.Peek();

        while (stack.Count > 0)
        {
            Node node = stack.Pop();
            if (node.CollectiveWeight < 420 && node.Collectivebenefit >= bestNode.Collectivebenefit)
            {
                bestNode = node;
            }
            if (node.index == 11)
            {
                continue;
            }
            
            int[] leftValue = new int[11];
            node.BinaryRepresentation.CopyTo(leftValue, 0);
            leftValue[node.index] = 1;

            Node leftChild = new Node(leftValue, Items, node.index + 1);
            stack.Push(leftChild);

            int[] rightValue = new int[11];
            node.BinaryRepresentation.CopyTo(rightValue, 0);
            Node rightChild = new Node(rightValue, Items, node.index + 1);

            if (rightChild.CollectiveWeight < 420)
            {
                stack.Push(rightChild);
            }

        }
    }
}
    

class Program
{
    
    static void Main(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        KnapsackBFS knap = new KnapsackBFS(@"C:\Users\Drevora\source\repos\KnapSackSol\KnapSackSol\KnapSackItems.txt");
        knap.GenerateTreeBFS();
        Console.WriteLine("BFS:");
        foreach (var item in knap.bestNode.Items)
        {
            Console.WriteLine($"ID: {item.ID + 1}");   
        }
        
        Console.WriteLine($"Collective Weight: {knap.bestNode.CollectiveWeight}, Benefit: {knap.bestNode.Collectivebenefit}");
        Console.WriteLine("(Expected Weight: 398. Expected Benefit: 308)");
        sw.Stop();

        Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        knap.GenerateTreeDFS();
        Console.WriteLine("DFS:");
        foreach (var item in knap.bestNode.Items)
        {
            Console.WriteLine($"ID: {item.ID + 1}");
        }
        Console.WriteLine($"Collective Weight: {knap.bestNode.CollectiveWeight}, Benefit: {knap.bestNode.Collectivebenefit}");
        Console.WriteLine("(Expected Weight: 419. Expected Benefit: 308)");
        sw.Stop();
        Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
    }
}
