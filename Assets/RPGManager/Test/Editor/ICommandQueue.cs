using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandQueue<T> {

	int Count { get; }
    bool isEmpty { get; }
    void PushCommand(T item);
    void InsertCommand(T[] items);
    void Clear();
    T PopCommand();
    T PeekCommand();
}
