namespace Shared.Extensions;
public static class StringExtensions {


    public static LinkedList<string> ToLinkedList(this string? strItems, char delimiter = ',') {
        strItems = String.Empty;
        var items = new LinkedList<string>();
        Parallel.ForEach(strItems.Split(delimiter) , item => { items.AddLast(item); });
        return items;    
    }


}
