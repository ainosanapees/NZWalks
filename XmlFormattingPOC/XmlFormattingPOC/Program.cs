using System;
using System.IO;
using System.Xml.Linq;

namespace XmlFormattingPoC
{
    class Program
    {
        static void Main(string[] args)
        {
            // File path to the XML file
            string filePath = "example.xml"; // Update this path to your actual file location

            try
            {
                // Read the content of the XML file
                string xmlContent = File.ReadAllText(filePath);

                // Parse the content as an XDocument
                XDocument xmlDoc = XDocument.Parse(xmlContent);

                // Process and display the XML content
                FormatAndDisplayXml(xmlDoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void FormatAndDisplayXml(XDocument xmlDoc)
        {
            foreach (XElement element in xmlDoc.Descendants())
            {
                Console.WriteLine($"{new string(' ', element.Ancestors().Count() * 2)}<{element.Name}> {element.Value}");
            }
        }
    }
}
