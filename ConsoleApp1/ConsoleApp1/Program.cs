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

            // Câu 4 – Truy vấn nhiều điều kiện
            Console.WriteLine("\n=== CÂU 4: SINH VIÊN NAM, TUỔI >= 25 THUỘC BỘ PHẬN CNTT ===");
            var c4 = dsSinhVien.Where(x => x.Tuoi >= 25 && x.GioiTinh == "Nam" && x.BoPhan == "CNTT");
            foreach (var sv in c4)
            {
                Console.WriteLine($"Mã SV: {sv.MaSV} | Họ tên: {sv.HoTen} | Tuổi: {sv.Tuoi} | Giới tính: {sv.GioiTinh} | Bộ phận: {sv.BoPhan}");
            }

            // Câu 5 – Sắp xếp
            Console.WriteLine("\n=== CÂU 5: DANH SÁCH SẮP XẾP THEO TUỔI GIẢM DẦN ===");
            var c5 = dsSinhVien.OrderByDescending(x => x.Tuoi);
            foreach (var sv in c5)
            {
                Console.WriteLine($"Mã SV: {sv.MaSV} | Họ tên: {sv.HoTen} | Tuổi: {sv.Tuoi}");
            }

            // Câu 6 – Sinh viên có tuổi cao nhất
            Console.WriteLine("\n=== CÂU 6: SINH VIÊN CÓ TUỔI CAO NHẤT ===");
            int maxTuoi = dsSinhVien.Max(x => x.Tuoi);
            var c6 = dsSinhVien.Where(x => x.Tuoi == maxTuoi);
            foreach (var sv in c6)
            {
                Console.WriteLine($"Mã SV: {sv.MaSV} | Họ tên: {sv.HoTen} | Nghề nghiệp: {sv.NgheNghiep} | Tuổi: {sv.Tuoi}");
            }

            // Câu 7 – Thống kê tuổi trung bình
            Console.WriteLine("\n=== CÂU 7: TUỔI TRUNG BÌNH THEO GIỚI TÍNH ===");
            double avgNam = dsSinhVien.Where(x => x.GioiTinh == "Nam").Average(x => x.Tuoi);
            double avgNu = dsSinhVien.Where(x => x.GioiTinh == "Nữ").Average(x => x.Tuoi);
            Console.WriteLine($"Tuổi trung bình của Nam: {avgNam:F1}");
            Console.WriteLine($"Tuổi trung bình của Nữ: {avgNu:F1}");

            // Câu 8 – Đếm
            Console.WriteLine("\n=== CÂU 8: ĐẾM SỐ SINH VIÊN CÓ TUỔI >= 20 VÀ CÒN LÀM VIỆC ===");
            int countC8 = dsSinhVien.Count(x => x.Tuoi >= 20 && x.TrangThai == "ConLamViec");
            Console.WriteLine($"Số lượng sinh viên thỏa điều kiện: {countC8}");

            // Câu 9 – Group By
            Console.WriteLine("\n=== CÂU 9: THỐNG KÊ SỐ LƯỢNG SINH VIÊN THEO BỘ PHẬN ===");
            var c9 = dsSinhVien.GroupBy(x => x.BoPhan)
                              .Select(g => new { BoPhan = g.Key, SoLuong = g.Count() });
            foreach (var group in c9)
            {
                Console.WriteLine($"Bộ phận: {group.BoPhan} - Số lượng: {group.SoLuong}");
            }

            // Câu 10 – Truy vấn cá nhân (Thống kê sinh viên theo quê quán)
            Console.WriteLine("\n=== CÂU 10: TRUY VẤN CÁ NHÂN (THỐNG KÊ SINH VIÊN THEO QUÊ QUÁN) ===");
            var c10 = dsSinhVien.GroupBy(x => x.QueQuan)
                               .Select(g => new { QueQuan = g.Key, SoLuong = g.Count(), DanhSach = string.Join(", ", g.Select(x => x.HoTen)) });
            foreach (var item in c10)
            {
                Console.WriteLine($"Quê quán: {item.QueQuan} ({item.SoLuong} người) -> Danh sách: {item.DanhSach}");
            }
            Console.ReadLine();
        }
    }
}