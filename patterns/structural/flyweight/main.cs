using System;
using System.Collections.Generic;

// ====== Flyweight \ intrinsic ======
public class BookType
{
    private string isbn;
    private string title;
    private string author;
    private string publisher;
    private string category;

    public BookType(string isbn, string title, string author, string publisher, string category)
    {
        this.isbn = isbn;
        this.title = title;
        this.author = author;
        this.publisher = publisher;
        this.category = category;
    }

    // set 
    public void setISBN(string isbn)
    {
        this.isbn = isbn;
    }

    public void setTitle(string title)
    {
        this.title = title;
    }

    public void setAuthor(string author)
    {
        this.author = author;
    }

    public void setPublisher(string publisher)
    {
        this.publisher = publisher;
    }

    public void setCategory(string category)
    {
        this.category = category;
    }

    // get
    public string getTitle()
    {
        return title;
    }

    public string getAuthor()
    {
        return author;
    }

    public string getPublisher()
    {
        return publisher;
    }

    public string getISBN()
    {
        return isbn;
    }

    public string getCategory()
    {
        return category;
    }

    public string formatDetails(string barcode, string location, string status)
    {
        return $"[Bercode: {barcode}](Status: {status} | Location: {location} ) \n" +
        $" - Title: {title} \n" +
        $" - Author: {author} \n" +
        $" - Publisher: {publisher} \n" +
        $" - Category: {category} \n" +
        $" - ISBN: {isbn} \n";
    }
}

// ====== Context \ Extrinsic ======
public class Book
{
    private string barcode;
    private string location;
    private string status;
    private BookType bookType;

    public Book(string barcode, string location, BookType bookType)
    {
        this.barcode = barcode;
        this.location = location;
        this.status = "Available";
        this.bookType = bookType;
    }

    // public bool checkStatusBook()
    // {
    //     if (status == "Available")
    //     {
    //         return true;
    //     }
    //     // else if (status == "Borrowed")
    //     // {
    //     //     return true;
    //     // }
    //     return false;
    // }

    public void borrowBook()
    {
        if (status == "Available")
        {
            this.status = "Borrowed";
            Console.WriteLine($">>[สำเร็จ]: หนังสือ {bookType.getTitle()} (barcode: {barcode}) ถูกยืมไปแล้ว");
        }
        else
        {
            Console.WriteLine($">>[Error]: ยืมไม่ได้เพราะว่าเล่มนี้มีคนยืมอยู่แล้ว status: {status}");
        }
    }

    public void returnBook()
    {
        if (status == "Borrowed")
        {
            this.status = "Available";
            Console.WriteLine($">>[สำเร็จ]: คืนหนังสือ {bookType.getTitle()} (barcode: {barcode}) แล้ว");
        }
        else
        {
            Console.WriteLine($">>[Error]: คืนหนังสือไม่ได้เพราะไม่ได้ยืม status: {status}");
        }
    }

    // public void borrowBook()
    // {
    //     if (status == "Available")
    //     {
    //         this.status = "Borrowed";
    //         Console.WriteLine($">>[สำเร็จ]: หนังสือ {bookType.getTitle()} (barcode: {barcode}) ถูกยืมไปแล้ว");
    //     }
    //     else
    //     {
    //         Console.WriteLine($">>[Error]: ยืมไม่ได้เพราะว่าเล่มนี้มีคนยืมอยู่แล้ว status: {status}");
    //     }
    // }

    // public void returnBook()
    // {
    //     if (status == "Borrowed")
    //     {
    //         this.status = "Available";
    //         Console.WriteLine($">>[สำเร็จ]: คืนหนังสือ {bookType.getTitle()} (barcode: {barcode}) แล้ว");
    //     }
    //     else
    //     {
    //         Console.WriteLine($">>[Error]: คืนหนังสือไม่ได้เพราะไม่ได้ยืม status: {status}");
    //     }
    // }

    public void getInfo()
    {
        Console.WriteLine(bookType.formatDetails(barcode, location, status));
    }

    //set
    public void setBarcode(string barcode)
    {
        this.barcode = barcode;
    }

    public void setLocation(string location)
    {
        this.location = location;
    }

    public void setStatus(string status)
    {
        this.status = status;
    }

    // get 
    public string getBarcode()
    {
        return barcode;
    }

    public string getLocation()
    {
        return location;
    }

    public string getStatus()
    {
        return status;
    }
}

// ====== BookFactory ======
public class BookFactory
{
    List<BookType> bookTypes = new List<BookType>();

    public BookFactory()
    {
        bookTypes.Add(new BookType("9786162877056", "ผ่อนคลายกับชีวิตบ้าง แล้วเธอจะชอบตัวเองมากขึ้น", "โคเซโกะ, โนบุยุกิ", "วีเลิร์น", "จิตวิทยา"));
        bookTypes.Add(new BookType("9786162878275", "ศิลปะแห่งการล่อลวงจิตใจคน = The art of seduction", "กรีน, โรเบิร์ต", "วีเลิร์น", "จิตวิทยา"));
        bookTypes.Add(new BookType("9786164344808", "ไบเบิล 101 (BIBLE 101)", "เอ็ดเวิร์ด เกรฟลีย์, ปีเตอร์ เจอาร์", "แอร์โรว์ มัลติมีเดีย", "ศาสนา"));
        bookTypes.Add(new BookType("9786160841622", "การเขียนโปรแกรม Swift และ iOS ฉบับพื้นฐาน", "บัญชา ปะสีละเตสัง", "ซีเอ็ดยูเคชั่น/se-ed", "คอมพิวเตอร์"));
    }
    public BookType getBookType(string isbn,
                                string title,
                                string author,
                                string publisher,
                                string category)
    {
        foreach (BookType item in bookTypes)
        {
            if (item.getISBN() == isbn &&
                item.getTitle() == title &&
                item.getAuthor() == author &&
                item.getPublisher() == publisher &&
                item.getCategory() == category)
            {
                return item;
            }
        }

        BookType newBookType = new BookType(isbn, title, author, publisher, category);
        bookTypes.Add(newBookType);

        return newBookType;
    }

    public void showBookTypeFactory()
    {
        Console.WriteLine("==== BookType in factory");
        foreach (var b in bookTypes)
        {
            Console.WriteLine(">> " + b.getISBN() + " " + b.getTitle() + " " + b.getAuthor() + " " + b.getPublisher() + " " + b.getCategory() + " ");
        }
        Console.WriteLine();
    }
}

// ====== Clietn ======

public class Library
{
    List<Book> books = new List<Book>();

    public void addBook(string barcode,
                        string location,
                        string isbn,
                        string title,
                        string author,
                        string publisher,
                        string category,
                        BookFactory f)
    {
        BookType type = f.getBookType(isbn, title, author, publisher, category);
        Book newBook = new Book(barcode, location, type);
        books.Add(newBook);
    }

    public void removeBookByBarcode(string barcode)
    {
        Book removeBook = null;

        // ค้นตาม barcode
        foreach (Book book in books)
        {
            if (barcode == book.getBarcode())
            {
                removeBook = book;
                break;
            }
        }

        // ถ้าเจอ
        if (removeBook != null)
        {
            books.Remove(removeBook);
            Console.WriteLine($">> ลบหนังสือบาร์โค้ด {barcode} ออกจากระบบเรียบร้อย");
        }
        else
        {
            Console.WriteLine($">> ไม่พบหนังสือบาร์โค้ด {barcode} ในระบบ");
        }
    }

    public bool borrowBookLibrary(string barcode)
    {
        foreach (Book book in books)
        {
            if (barcode == book.getBarcode())
            {
                book.borrowBook();
                return true;
            }
        }

        return false;
    }

    public bool returnBookLibrary(string barcode)
    {
        foreach (Book book in books)
        {
            if (barcode == book.getBarcode())
            {
                book.returnBook();
                return true;
            }
        }

        return false;
    }

    // public bool returnBookLibrary(string barcode)
    // {
    //     foreach (Book book in books)
    //     {
    //         if (barcode == book.getBarcode())
    //         {
    //             book.returnBook();
    //             return true;
    //         }
    //     }

    //     return false;
    // }

    public void showBookAll()
    {
        foreach (Book book in books)
        {
            book.getInfo();
        }
    }
}

public class Program
{
    static void Main()
    {
        BookFactory bookFactory = new BookFactory();

        bookFactory.showBookTypeFactory();

        Library library = new Library();

        library.addBook("001", "A1-2", "9786168381007", "สารคดีปิดตาย", "Fake Documentary Q", "Bibli (บิบลิ)", "นิยายแปล", bookFactory); // New Type
        library.addBook("002", "B1-2", "9786160841622", "การเขียนโปรแกรม Swift และ iOS ฉบับพื้นฐาน", "บัญชา ปะสีละเตสัง", "ซีเอ็ดยูเคชั่น/se-ed", "คอมพิวเตอร์", bookFactory); // Old Type
        library.addBook("003", "C1-2", "9786168296516", "คู่มือนิติเวชศาสตร์ ฉบับพกพา", "กิตติศักดิ์ คงคา", "13357 PUBLISHIHG", "หนังสือบทความ สารคดี", bookFactory); // New Type
        library.addBook("004", "D1-3", "9786162878275", "ศิลปะแห่งการล่อลวงจิตใจคน = The art of seduction", "กรีน, โรเบิร์ต", "วีเลิร์น", "จิตวิทยา", bookFactory); // Old Type
        library.addBook("005", "A1-2", "9786168381007", "สารคดีปิดตาย", "Fake Documentary Q", "Bibli (บิบลิ)", "นิยายแปล", bookFactory); // old type

        library.showBookAll();
        bookFactory.showBookTypeFactory();

        Console.WriteLine();

        Console.WriteLine("นาย สมชัย ยืมหนังสือ: 005");
        if (library.borrowBookLibrary("005"))
        {
            Console.WriteLine(">>ยืมสำเร็จ");
        }
        else
        {
            Console.WriteLine(">>ยืมไม่สำเร็จ");
        }

        Console.WriteLine();
        library.showBookAll();
        Console.WriteLine();
        Console.WriteLine("นาย สมชัย คืนหนังสือ: 005");
        if (library.returnBookLibrary("005"))
        {
            Console.WriteLine(">>คืนสำเร็จ");
        }
        else
        {
            Console.WriteLine(">>คืนไม่สำเร็จ");
        }
        Console.WriteLine();
        library.showBookAll();
    }
}
