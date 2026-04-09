using System;

//--- strategy
public interface languageStrategy
{
    void runCode(string filename);
}

//--- concrete strategy
public class GoStrategy : languageStrategy
{
    private string version;
    private string compiler;

    public GoStrategy(string version, string compiler)
    {
        this.compiler = compiler;
        this.version = version;
    }

    public void runCode(string filename)
    {
        Console.WriteLine("---------------------------");
        Console.WriteLine($"- go version: {version}");
        Console.WriteLine($"- compiler: {compiler}");
        Console.WriteLine("---------------------------");
        Console.WriteLine($">> go run {filename}.go");
        Console.WriteLine();
    }

    //-- set
    public void setVersion(string version)
    {
        this.version = version;
    }

    public void setCompiler(string compiler)
    {
        this.compiler = compiler;
    }

    //-- get 
    public string getVersion()
    {
        return version;
    }

    public string getCompiler()
    {
        return compiler;
    }
}

public class CppStrategy : languageStrategy
{
    private string version;
    private string compiler;

    public CppStrategy(string version, string compiler)
    {
        this.compiler = compiler;
        this.version = version;
    }

    public void runCode(string filename)
    {
        Console.WriteLine("---------------------------");
        Console.WriteLine($"- go version: {version}");
        Console.WriteLine($"- compiler: {compiler}");
        Console.WriteLine("---------------------------");
        Console.WriteLine($">> {compiler} {filename}.cpp");
        Console.WriteLine($">> ./a.out");
        Console.WriteLine();
    }

    //-- set
    public void setVersion(string version)
    {
        this.version = version;
    }

    public void setCompiler(string compiler)
    {
        this.compiler = compiler;
    }

    //-- get 
    public string getVersion()
    {
        return version;
    }

    public string getCompiler()
    {
        return compiler;
    }
}

public class CSharpStrategy : languageStrategy
{
    private string version;
    private string compiler;

    public CSharpStrategy(string version, string compiler)
    {
        this.compiler = compiler;
        this.version = version;
    }

    public void runCode(string filename)
    {
        Console.WriteLine("---------------------------");
        Console.WriteLine($"- go version: {version}");
        Console.WriteLine($"- compiler: {compiler}");
        Console.WriteLine("---------------------------");
        Console.WriteLine($">> dotnet {filename}.cs");
        Console.WriteLine();
    }

    //-- set
    public void setVersion(string version)
    {
        this.version = version;
    }

    public void setCompiler(string compiler)
    {
        this.compiler = compiler;
    }

    //-- get 
    public string getVersion()
    {
        return version;
    }

    public string getCompiler()
    {
        return compiler;
    }
}

//--- Context
public class CodeEditor
{
    private languageStrategy strategy;
    private string name;

    public CodeEditor(string name)
    {
        this.name = name;
    }

    public CodeEditor(string name, languageStrategy strategy)
    {
        this.name = name;
        this.strategy = strategy;
    }

    public void setStrategy(languageStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void buttonRun(string filename)
    {
        strategy.runCode(filename);
    }

    //-- set
    public void setName(string name)
    {
        this.name = name;
    }

    //-- get 
    public string getName()
    {
        return name;
    }
}

public class Program
{
    static void Main()
    {
        CodeEditor vsCode = new CodeEditor("Visual studio code");
        vsCode.setStrategy(new GoStrategy("1.13", "gc"));
        vsCode.buttonRun("main");
        vsCode.buttonRun("main_2");

        vsCode.setStrategy(new CppStrategy("23", "g++"));
        vsCode.buttonRun("hello");
        vsCode.setStrategy(new CppStrategy("23", "clang++"));
        vsCode.buttonRun("hello_2");

        vsCode.setStrategy(new CSharpStrategy(".NET v10", "dotnet"));
        vsCode.buttonRun("index");
    }
}