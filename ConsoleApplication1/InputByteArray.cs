// Copyright (c) 2018 ActivePDF, Inc.
// ActivePDF Toolkit 2018
// Example generated 05/02/18 

using System;

// Make sure to add the ActivePDF product .NET DLL(s) to your application.
// .NET DLL(s) are typically found in the products 'bin' folder.

class Examples
{
  public static void Example()
  {
    int intOpenOutputFile;
    string strPath;
    int intOpenInputFile;
    int intCopyForm;
    string memPDF;
    int intMergeFile;
    
    strPath = System.AppDomain.CurrentDomain.BaseDirectory;

    // Instantiate Object
    APToolkitNET.Toolkit oTK = new APToolkitNET.Toolkit();
    
    // Here you can place any code that will alter the output file
    // Such as adding security, setting page dimensions, etc.
    
    APToolkitNET.FieldInfo oFI = oTK.FieldInfo("name", 1);  
    
    string TKversion = oTK.ToolkitVersion;
    
 
    // Create the new PDF file in memory
    intOpenOutputFile = oTK.OpenOutputFile("MEMORY");
    if (intOpenOutputFile != 0)
    {
      ErrorHandler("OpenOutputFile", intOpenOutputFile);
    }
  
    //convert pdf to byte array
    byte[] bytes = System.IO.File.ReadAllBytes(strPath + "form.pdf");
    
    oTK.InputByteArray = bytes;



    // Open the template PDF
    intOpenInputFile = oTK.OpenInputFile("MEMORY");
    if (intOpenInputFile != 0)
    {
      ErrorHandler("OpenInputFile", intOpenInputFile);
    }


    // Here you can call any Toolkit functions that will manipulate
    // the input file such as text and image stamping, form filling, etc.


    short formCount = oTK.CountFormFields();

    Console.WriteLine(TKversion + "Count form fields = " + formCount);

    Console.ReadKey();


    // Copy the template (with any changes) to the new file
    // Start page and end page, 0 = all pages
    intCopyForm = oTK.CopyForm(0, 0);
    if (intCopyForm != 1)
    {
      ErrorHandler("CopyForm", intCopyForm);
    }
    
    // Close the new file to complete PDF creation
    oTK.CloseOutputFile();
    
    // Set the in memory PDF to a variable
    // To retrieve the PDF as a byte array use oTK.BinaryImage
    memPDF = oTK.OutputByteStream;
    
    // Toolkit can take a PDF in memory and use it as an input file
    // Here we will use the PDF we just created in memory
    
    // Create the final PDF on disk
    intOpenOutputFile = oTK.OpenOutputFile(strPath + "final.pdf");
    if (intOpenOutputFile != 0)
    {
      ErrorHandler("OpenOutputFile", intOpenOutputFile);
    }
    
    // Prepare the in memory PDF to be used with Toolkit
    // For .NET Toolkit also has InputByteArray to accept Byte Arrays
    oTK.InputByteStream = memPDF;
    
    // Now we can use 'MEMORY' as the filename with OpenInputFile or MergeFile
    intMergeFile = oTK.MergeFile("MEMORY", 0, 0);
    if (intMergeFile != 1)
    {
      ErrorHandler("MergeFile", intMergeFile);
    }
    
    // Close the final file to complete PDF creation
    oTK.CloseOutputFile();
    
    // To save a PDF in memory to a file directly call SaveMemoryToDisk
    oTK.SaveMemoryToDisk(strPath + "SavedMemory.pdf");
    
    // Release Object
    oTK.Dispose();
    
    // Process Complete
    WriteResults("Done!");
  }
  
  // Error Handling
  public static void ErrorHandler(string strMethod, object rtnCode)
  {
    WriteResults(strMethod + " error:  " + rtnCode.ToString());
  }
  
  // Write output data
  public static void WriteResults(string content)
  {
    // Choose where to write out results
  
    // Debug output
    //System.Diagnostics.Debug.WriteLine("ActivePDF: * " + content);
  
    // Console
    Console.WriteLine(content);
  
    // Log file
    //using (System.IO.TextWriter writer = new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "application.log", true))
    //{
    //    writer.WriteLine("[" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "]: => " + content);
    //}
  }
}