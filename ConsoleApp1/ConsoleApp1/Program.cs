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

            // Câu 2 – Truy vấn danh sách
            Console.WriteLine("\n=== CÂU 2: TRUY VẤN MÃ SV, HỌ TÊN VÀ BỘ PHẬN ===");
            var c2 = dsSinhVien.Select(x => new { x.MaSV, x.HoTen, x.BoPhan });
            foreach (var sv in c2)
            {
                Console.WriteLine($"Mã SV: {sv.MaSV} - Họ tên: {sv.HoTen} - Bộ phận: {sv.BoPhan}");
            }

            // Câu 3 – Truy vấn có điều kiện (Tuổi từ 20 đến 35)
            Console.WriteLine("\n=== CÂU 3: SINH VIÊN CÓ ĐỘ TUỔI TỪ 20 ĐẾN 35 ===");
            var c3 = dsSinhVien.Where(x => x.Tuoi >= 20 && x.Tuoi <= 35);
            foreach (var sv in c3)
            {
                Console.WriteLine($"Mã SV: {sv.MaSV} | Họ tên: {sv.HoTen} | Tuổi: {sv.Tuoi}");
            }

            Console.ReadLine();
        }
    }
}