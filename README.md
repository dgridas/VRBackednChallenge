Alongside this challenge, you will also find a [sample data file](data.txt).

This is a sample of an ASN (Acknowledgement Shipping Notification) message that we receive from one of our suppliers.
Each HDR section, represents a box, and the lines below the HDR section describe the contents of the box.
When we reach another HDR section, it means that there is another box and we repeat the process from the beginning.

## Data file structure
<pre>
HDR  TRSP117                                           6874454I                           
LINE P000001661         9781465121550         12     
LINE P000001661         9925151267712         2      
LINE P000001661         9651216865465         1      
</pre>

## Description
<pre>
HDR             - just a keyword telling that a new box is being described.
TRSP117         - Supplier identifier.
6874454I        - Carton box identifier. Displayed on the box.
LINE            - keyword to identify product item in the box.
P000001661      - Our PO Number that we sent to the supplier.
9781465121550   - ISBN 13 (product barcode).
12              - Product quantity.
</pre>

The solution should monitor a specific file path, and whenever a file is dropped in that folder, the file should be parsed and loaded into a database.
The file could be very large and exceed the available RAM.

IMPORTANT: The task you are going to resolve is used to estimate your approach and solution.

    The following code class is merely an example to demonstrate the structure. You should modify it as needed to achieve your goal.
    If you have any question about the task, please feel free to ask.

```csharp
public class Box
{
    public string SupplierIdentifier { get; set; }
    public string Identifier { get; set; }

    public IReadOnlyCollection<Content> Contents { get; set; } 

    public class Content
    {
        public string PoNumber { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }
    }
}
```
We look forward to seeing your solution!