using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AracListesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ShowList();
        }

        List<Arac> Aracs { get; set; } = new List<Arac>()
        {
            new Arac()
           {
               Ad = "Alper",
               soyad = "Ünal",
               Email = "alperunal001@gmail.com",
               telno = "0555 555 55 55",
               adres = "Keçiören",
               sehir = "Ankara",
               aracmarka = "BMW",
               aracmodel = "F10",
               yakıtturu = "benzin",
               renk = "siyah",
               vites = "manuel",
               kasatipi = "sedan",
               km = "70.000",
               acıklama= "BMW,F10,SİYAH,MANUEL,70.000"
           },
             new Arac()
           {
               Ad = "Ayşe",
               soyad = "Kara",
               Email = "aysekara002@gmail.com",
               telno = "0506 666 66 66",
               adres = "mamak",
               sehir = "Ankara",
               aracmarka = "MERCEDES",
               aracmodel = "C200",
               yakıtturu = "benzin",
               renk = "beyaz",
               vites = "otomatik",
               kasatipi = "sedan",
               km = "55.000",
               acıklama= "MERCEDES,C200,BEYAZ,OTOMATİK,55.000"
           },
        };

        public void ShowList()
        {
            listView1.Items.Clear();
            foreach (Arac arac in Aracs)
            {
                AddAracToListView(arac);
            }
        }

        public void AddAracToListView(Arac arac)
        {
            ListViewItem item = new ListViewItem(new string[]
                {
                    arac.Ad,
                    arac.soyad,
                    arac.Email,
                    arac.telno,
                    arac.adres,
                    arac.sehir,
                    arac.aracmarka,
                    arac.aracmodel,
                    arac.yakıtturu,
                    arac.renk,
                    arac.vites,
                    arac.kasatipi,
                    arac.km,
                    arac.acıklama,

                });
            item.Tag = arac;
            listView1.Items.Add(item);

        }

        void EditAracOnListView(ListViewItem aItem, Arac arac)
        {
            aItem.SubItems[0].Text = arac.Ad;
            aItem.SubItems[1].Text = arac.soyad;
            aItem.SubItems[2].Text = arac.Email;
            aItem.SubItems[3].Text = arac.telno;
            aItem.SubItems[4].Text = arac.adres;
            aItem.SubItems[5].Text = arac.sehir;
            aItem.SubItems[6].Text = arac.aracmarka;
            aItem.SubItems[7].Text = arac.aracmodel;
            aItem.SubItems[8].Text = arac.yakıtturu;
            aItem.SubItems[9].Text = arac.renk;
            aItem.SubItems[10].Text = arac.vites;
            aItem.SubItems[11].Text = arac.kasatipi;
            aItem.SubItems[12].Text = arac.km;
            aItem.SubItems[13].Text = arac.acıklama;

            aItem.Tag = arac;
        }
       
        private void AddCommand(object sender, EventArgs e)
        {
            FrmArac frm = new FrmArac() 
            { Text = "Kişi Ekle",
            StartPosition = FormStartPosition.CenterParent,
              arac = new Arac() };
           

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Aracs.Add(frm.arac);
                AddAracToListView(frm.arac);
            }

        }

        private void EditCommand(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            ListViewItem aItem = listView1.SelectedItems[0];


            Arac secili = aItem.Tag as Arac;


            FrmArac frm = new FrmArac()
            {
                Text = "Kişi Düzenle",
                StartPosition = FormStartPosition.CenterParent,
                arac = Clone(secili),
            };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                secili = frm.arac;
                EditAracOnListView(aItem, secili);

            }
        }

        Arac Clone(Arac arac)
        {
            return new Arac()
            {
                id = arac.id,
                Ad = arac.Ad,
                soyad = arac.soyad,
                Email = arac.Email,
                telno = arac.telno,
                adres = arac.adres,
                sehir = arac.sehir,
                aracmarka = arac.aracmarka,
                aracmodel = arac.aracmodel,
                yakıtturu = arac.yakıtturu,
                renk = arac.renk,
                vites = arac.vites,
                kasatipi = arac.kasatipi,
                km = arac.km,
                acıklama = arac.acıklama
            };
        }

        private void DeleteCommand(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            ListViewItem aItem = listView1.SelectedItems[0];

            Arac secili = aItem.Tag as Arac;

           var sonuc = MessageBox.Show($"Seçili kişi silinsin mi ? \n\n{secili.Ad} {secili.soyad}",
                "Silmeyi Onayla", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question );

            if (sonuc == DialogResult.Yes )
            {
                Aracs.Remove( secili );
                listView1.Items.Remove( aItem );

            }
        }

        private void SaveCommand(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog()
            {
                Filter = "Json Formatı|*.json|Xml Formatı|*.xml"

            };

            if(sf.ShowDialog() == DialogResult.OK)
            {
                if (sf.FileName.EndsWith("json"))
                {
                   String data = JsonSerializer.Serialize(Aracs);
                    File.WriteAllText(sf.FileName, data );
                }
                else if(sf.FileName.EndsWith("xml"))
                {
                    StreamWriter sw = new StreamWriter(sf.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Arac>));  
                    serializer.Serialize(sw, Aracs);
                    sw.Close();
                }
            }
        }

    }


    [Serializable]

    public class Arac
    {
        public string id;

        public string ID
        {
            get
            {
                if (id != null)
                    id = Guid.NewGuid().ToString();
                return id;
            }
            set { id = value; }
        }
        public string Ad { get; set; }
        public string soyad { get; set; }
        public string Email { get; set; }
        public string telno { get; set; }
        public string adres { get; set; }
        public string sehir { get; set; }
        public string aracmarka { get; set; }
        public string aracmodel { get; set; }
        public string yakıtturu { get; set; }
        public string renk { get; set; }
        public string vites { get; set; }
        public string kasatipi { get; set; }
        public string km { get; set; }
        public string acıklama { get; set; }
    }
}
