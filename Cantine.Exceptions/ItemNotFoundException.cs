namespace Cantine.Exceptions;

/// <summary>
/// This class describes the base exception triggers when an item of type was not found
/// </summary>
/// <typeparam name="T">The type of the item</typeparam>
public abstract class ItemNotFoundException<T>() : Exception($"The item of type {typeof(T)} was not found.");