using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several values with different priorities, added out of priority order:
    // A(1), B(2), C(5), D(3). Note the highest priority item (C) is NOT the last one added,
    // and the last one added (D) is NOT the highest priority - this specifically targets the
    // last-index-of-the-list edge case.
    // Expected Result: Dequeuing repeatedly returns values from highest priority to lowest:
    // C, D, B, A.
    // Defect(s) Found: The for-loop condition "index < _queue.Count - 1" skipped the last
    // element of the list entirely, so it was never able to be removed.
    // Also, Dequeue never removed the item from the list, so the
    // queue never shrank and the same highest-priority item could be returned repeatedly.
    public void TestPriorityQueue_Dequeue_HighestPriorityFirst()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 3);

        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue several values where multiple items share the same highest priority:
    // A(1), B(3), C(3), D(2), E(3). B, C, and E are tied for highest priority (3), added in
    // that order.
    // Expected Result: Dequeue should resolve ties using FIFO order (item closest to the front
    // of the queue comes out first): B, C, E, then D, then A.
    // Defect(s) Found: The comparison used ">=" instead of ">" when searching for the highest
    // priority index. This means that on a tie, a later item in the list would overwrite an
    // earlier one as the "highest priority" candidate, causing the LAST item would be dequeued first.
    public void TestPriorityQueue_Dequeue_TiesResolvedByFifoOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 2);
        priorityQueue.Enqueue("E", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("E", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two values, A(1) and B(5). B has the higher priority. Enqueue does not
    // reorder anything, so the underlying storage should still list them in insertion order.
    // Expected Result: ToString() reflects insertion order (back-of-queue insertion), not
    // priority order: "[A (Pri:1), B (Pri:5)]".
    // Defect(s) Found: None.
    public void TestPriorityQueue_Enqueue_AddsToBackRegardlessOfPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);

        Assert.AreEqual("[A (Pri:1), B (Pri:5)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Call Dequeue on a queue that has had every item removed (i.e., is empty).
    // Expected Result: An InvalidOperationException is thrown with the message
    // "The queue is empty."
    // Defect(s) Found: None.
    public void TestPriorityQueue_Dequeue_EmptyQueueThrowsException()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Dequeue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Dequeue every item out of a queue and verify the queue is truly emptied (each
    // value is only returned once, and the list shrinks with each Dequeue call).
    // Expected Result: ToString() shows an empty list "[]" after all items have been dequeued.
    // Defect(s) Found: Dequeue never called RemoveAt, so items were never actually removed from
    // the list. The queue would never become empty and ToString() would keep showing
    // the original items.
    public void TestPriorityQueue_Dequeue_RemovesItemsFromQueue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);

        priorityQueue.Dequeue();
        priorityQueue.Dequeue();

        Assert.AreEqual("[]", priorityQueue.ToString());
    }
}