using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;




namespace XmlApp
{
   


    static class Constants  //constants
    {
        public const int FileNum = 10;          //  number of file 
        public const int FileIncNum = 100;      // # of number included by files 
        public const string fileName = "file";
        

    }

   

    class Program
    {
       

        //fonksiyonları ve parametlerini daha verimli hale getir.

        //Daha kullanışlı bir yapı kurmaya çalış.

        //sürekli tekrarlayan yapıları/değişkenleri Global hale getir.

        //sürekli tekrarlayan işlemleri bir fonksiyon haline getir.

        





        static Boolean CreateFile(int Numfile,int NumInc,string fileName)
        {
            Random Rnd = new Random(); //create random values

            for (int i = 1; i <= Numfile; i++)
            {
               try
                {
                    //dosya yolunu ve adını global hale getirmeye çalış.Başka fonksiyonlarda da kullanılabilsin.daha iyi olur.

                    //create xml file
                     XmlTextWriter wr = new XmlTextWriter(fileName+i.ToString()+".xml", System.Text.UTF8Encoding.UTF8);

                    wr.WriteStartDocument();           //declaration for xml file
                    wr.WriteStartElement("sayiList");  //create first element
                    wr.WriteEndElement();              
                    wr.Close();


                    //load xml file
                    XmlDocument _data = new XmlDocument();
                    _data.Load("file"+i.ToString()+".xml");

                   

                    //write random numbers in that xml file
                    for (int j=1;j<= NumInc;j++)
                    {
                        int num = Rnd.Next(1, NumInc); //create new  random number  every for j value

                        XmlElement _element = _data.CreateElement("Sayi");  //create element <Sayi></Sayi>
                        _element.SetAttribute("index", j.ToString());       //setAttribute  <Sayi index="NUM"><Sayi>
                        _element.InnerText = num.ToString();

                        _data.DocumentElement.AppendChild(_element);
                    }  //end of for j

                   
                    XmlTextWriter _write = new XmlTextWriter(fileName+ i.ToString() + ".xml", null);
                    _write.Formatting = Formatting.Indented;
                    _data.WriteContentTo(_write);
                    _write.Close();

                  
                } //end of try
                catch
                {
                    return false;

                } //end of catch

            } //end of for i

            return true;
        } //end of createFile function

        
        static Boolean XmlFileSort(string fileName,int chosen)  //xml file to array for sorting
        {
            try
            {


                for (int i = 1; i<=10; i++)
                {
                    var document = XDocument.Load(fileName + i.ToString() + ".xml");  //load xml file
                    var array = document.Descendants("Sayi").Select(x => (int)x).ToArray();  // xml file to array


                    //---1 for SELECTİON SORT---
                    if (chosen == 1)
                    {
                   
                        int temp, min;

                        for (int a = 0; a < array.Length - 1; a++)
                        {
                            min = a;
                            for (int j = a + 1; j < array.Length; j++)
                            {
                                if (array[j] < array[min])
                                    min = j;

                            }
                            if (min != a)
                            {
                                temp = array[a];
                                array[a] = array[min];
                                array[min] = temp;
                            }

                        }  

                } //end of if selection sort


                    //---2 for BUBBLE SORT ---

                    if (chosen == 2)
                    {
                        int a = 1, j, val;
                      
                        while (a < array.Length)
                        {
                            j = array.Length - 1;
                            while (j >= 1)
                            {
                                if (array[j - 1] > array[j])
                                {
                                    val = array[j];
                                    array[j] = array[j - 1];
                                    array[j - 1] = val;
                                }
                                j--;
                            }
                            a++;
                        }

                    } //end of if  Bubble sort




                    //3 for INSERTION SORT

                    if (chosen == 3)
                    {
                        for (int a= 0; a <array.Length; a++)
                        {
                            for (int j = a; j > 0; j--)
                            {
                                if (array[j] < array[j - 1])
                                {
                                    int temp = array[j];
                                    array[j] = array[j - 1];
                                    array[j - 1] = temp;
                                }
                            }
                        }

                    } // end of if Insertion sort



                    //write sorted data into xml file
                    //create new xml file

                XmlTextWriter wr = new XmlTextWriter(fileName + i.ToString() + ".xml", System.Text.UTF8Encoding.UTF8);

                    wr.WriteStartDocument();           //declaration for xml file
                    wr.WriteStartElement("sayiList");  //create first element
                    wr.WriteEndElement();
                    wr.Close();


                    //load xml file
                    XmlDocument _data = new XmlDocument();
                    _data.Load("file" + i.ToString() + ".xml");

                    for (int j = 0; j <array.Length; j++)
                    {
                       
                        XmlElement _element = _data.CreateElement("Sayi");  //create element <Sayi></Sayi>
                        _element.SetAttribute("index", j.ToString());       //setAttribute  <Sayi index="NUM"><Sayi>
                        _element.InnerText = array[j].ToString();
                        _data.DocumentElement.AppendChild(_element);
                    }  //end of for j

                    XmlTextWriter _write = new XmlTextWriter(fileName + i.ToString() + ".xml", null);
                    _write.Formatting = Formatting.Indented;
                    _data.WriteContentTo(_write);
                    _write.Close();


                }  //end of for i


            } //end of try
            catch{
                
                return false;
            } //end of catch

            return true;
            
        } //end of XmlFileSort function




        static Boolean FindCountOfData(string fileName,int fileNum,int NumInc)
        {
            int count = 0, indexx =0;

            int[] temp = new int[fileNum * NumInc];  // set numbers from all xml files


            try
            {

                XmlTextWriter wr = new XmlTextWriter("Counter.xml", System.Text.UTF8Encoding.UTF8);

                wr.WriteStartDocument();           //declaration for xml file
                wr.WriteStartElement("sortedList");  //create first element
                wr.WriteEndElement();
                wr.Close();

                //load xml file
                XmlDocument _data = new XmlDocument();
                _data.Load("Counter.xml");



                //şimdilik tek boyutlu bir dizi oluşturup ,tüm dosyalardaki sayıları o  diziye atmayı dene.Sonra dizi içerisinde tekrar edenleri bulursun ama daha sonra  iyi bir çözüm bulmaya çalış.


                for (int i = 1; i <= fileNum; i++)
                {
                    var document = XDocument.Load(fileName + i.ToString() + ".xml");  //load xml 
                    var array = document.Descendants("Sayi").Select(x => (int)x).ToArray();  // xml file to array

                    for(int j = 0; j < array.Length; j++)
                    {
                       
                        temp[indexx] = array[j]; //all number in xml files copy into temp array
                        indexx++;
                    }
    
                }  //end of for i

                Console.WriteLine(temp[100]);
                //Count

                //
               for(int j = 0; j < indexx; j++)
                {
                    for(int k=0;k< indexx-1; k++)
                    {

                        if (temp[j] == temp[k+1])
                        {
                            count++;
                        } //end for k

                       
                    } //end of for j

                    XmlElement _element = _data.CreateElement("Sayi");  //create element <Sayi></Sayi>
                    _element.SetAttribute("index", j.ToString());       //setAttribute  <Sayi index="NUM"><Sayi>
                    _element.SetAttribute("tekrarAdet", (count-1).ToString());       // tekrar eden sayının kendisini çıkar.
                    _element.InnerText = temp[j].ToString();
                    _data.DocumentElement.AppendChild(_element);



                } //end of for i

                XmlTextWriter _write = new XmlTextWriter("Counter.xml", null);
                _write.Formatting = Formatting.Indented;
                _data.WriteContentTo(_write);
                _write.Close();


            }//end of try
            catch
            {

                return false;
            }

            return true;
        } // end of  findCountOfData function




         static void Main(string[] args)
        {
           if( CreateFile(Constants.FileNum,Constants.FileIncNum,Constants.fileName))
            {

                

                Console.WriteLine("---Xml file created succesfully--");
                Console.WriteLine("--For Selection sort     ->1 enter");
                Console.WriteLine("--For Bubble  sort       ->2 enter");
                Console.WriteLine("--For İnsertion sort     ->3 enter");

                int a = Convert.ToInt32(Console.ReadLine());

                if (a == 1||a == 2||a == 3)
                {
                    if (XmlFileSort(Constants.fileName, a))  {
                        Console.WriteLine("Xml files succesfuly sorted. Please wait for Encounter");

                        if (FindCountOfData(Constants.fileName,Constants.FileNum,Constants.FileIncNum))
                        {
                            Console.WriteLine("Counter.xml is Ready!");
                        }
                        else
                        {
                            Console.WriteLine("Exception error occured while recurring numbers were encountered");

                        }
                        
                        
                    }
                    else {
                        Console.WriteLine("Error! Xml files can not sort"); } 

                }  //end of if  that control a between 1-3
                else {
                    Console.WriteLine("Select correct input"); }
                   
               
            } // end of if that control creating xml files
            else{
                Console.WriteLine("Exception error while creating xml files");
            }



            Console.Read(); 

        } //end of Main

    } //end of class



}
