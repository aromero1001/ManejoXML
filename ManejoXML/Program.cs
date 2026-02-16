using System.Xml;
using System.IO;

string root = AppContext.BaseDirectory;
var path = Path.Combine(root, "", "CustomersOrders.xml");

// Validación de seguridad antes de entrar al Heap
if (!File.Exists(path))
{
    Console.WriteLine($"Error: No se encontró el archivo en {path}");
    return;
}

// Llamada al método (ahora sin static para que sea visible)
UsingXmlReader(path);

void UsingXmlReader(string xmlPath)
{
    using XmlReader xmlReader = XmlReader.Create(xmlPath);

    while (xmlReader.Read())
    {
        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Customer")
        {
            string id = xmlReader.GetAttribute("CustomerID") ?? "N/A";

            using (XmlReader inner = xmlReader.ReadSubtree())
            {
                string company = "";
                string contact = "";

                while (inner.Read())
                {
                    if (inner.NodeType == XmlNodeType.Element)
                    {
                        if (inner.Name == "CompanyName") company = inner.ReadElementContentAsString();
                        if (inner.Name == "ContactName") contact = inner.ReadElementContentAsString();
                    }
                }

                Console.WriteLine($"[{id}] {company} -> Contacto: {contact}");
            }
        }
    }
}

void UsingXmlWriter()
{
    XmlWriter xmlWriter = XmlWriter.Create("Contact.xml");

    xmlWriter.WriteStartDocument();

    xmlWriter.WriteStartElement("Contacts");

    xmlWriter.WriteStartElement("Contact");

    xmlWriter.WriteAttributeString("Phone", "022354");

    xmlWriter.WriteString("John");

    xmlWriter.WriteEndElement();

    xmlWriter.WriteEndDocument();

    xmlWriter.Close();
}


UsingXmlWriter();