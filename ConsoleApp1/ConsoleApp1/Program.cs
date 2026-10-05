using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BaiKiemTraXML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string filePath = @"D:\Hoc_Tap\Chuyen_nganh\HK_126\Thuc_hanh\Git\KiemTraXML1\SinhVien.xml";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Không tìm thấy file XML tại: {Path.GetFullPath(filePath)}");
                return;
            }

            XDocument doc = XDocument.Load(filePath);
            int currentYear = DateTime.Now.Year;

            // Đọc dữ liệu sinh viên
            var dsSinhVien = doc.Descendants("SinhVien").Select(x => new
            {
                MaSV = (string)x.Element("MaSV"),
                HoTen = (string)x.Element("HoTen"),
                BoPhan = (string)x.Element("BoPhan"),
                GioiTinh = (string)x.Element("GioiTinh"),
                NgaySinh = DateTime.Parse((string)x.Element("NgaySinh")),
                Tuoi = currentYear - DateTime.Parse((string)x.Element("NgaySinh")).Year,
                NgheNghiep = (string)x.Element("NgheNghiep"),
                QueQuan = (string)x.Element("QueQuan"),
                TrangThai = (string)x.Element("TrangThai")
            }).ToList();

            // Câu 1 – Đọc dữ liệu
            Console.WriteLine("=== CÂU 1: ĐỌC DỮ LIỆU CẢ DANH SÁCH SINH VIÊN ===");
            foreach (var sv in dsSinhVien)
            {
                Console.WriteLine($"Mã SV: {sv.MaSV} | Họ tên: {sv.HoTen} | Bộ phận: {sv.BoPhan} | Giới tính: {sv.GioiTinh} | Ngày sinh: {sv.NgaySinh:dd/MM/yyyy} ({sv.Tuoi} tuổi) | Nghề: {sv.NgheNghiep} | Quê: {sv.QueQuan}");
            }

  
           

            Console.ReadLine();
        }
    }
}