using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Models;

namespace LamHaiTest_UserManager.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng cần xem chi tiết!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại trong hệ thống!";
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserCode,FullName,BirthDate,Email,Phone,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm người dùng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi khi thêm người dùng. Vui lòng thử lại!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu nhập chưa hợp lệ. Vui lòng kiểm tra lại!";
            }

            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng cần chỉnh sửa!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại trong hệ thống!";
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserCode,FullName,BirthDate,Email,Phone,Address")] User user)
        {
            if (id != user.Id)
            {
                TempData["ErrorMessage"] = "ID người dùng không khớp!";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật thông tin người dùng thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        TempData["ErrorMessage"] = "Người dùng không tồn tại hoặc đã bị xóa!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Lỗi đồng bộ dữ liệu khi cập nhật. Vui lòng thử lại!";
                        throw;
                    }
                }
                catch
                {
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình cập nhật người dùng!";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu nhập chưa hợp lệ. Vui lòng kiểm tra lại!";
            }

            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng cần xóa!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại trong hệ thống!";
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Xóa người dùng thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy người dùng để xóa!";
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi xóa người dùng!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckUserCode(string userCode, int id)
        {
            bool exists = _context.Users.Any(u => u.UserCode == userCode && u.Id != id);
            if (exists)
            {
                return Json($"Mã người dùng '{userCode}' đã tồn tại!");
            }
            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email, int id)
        {
            bool exists = _context.Users.Any(u => u.Email == email && u.Id != id);
            if (exists)
            {
                return Json($"Email '{email}' đã được sử dụng!");
            }
            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckPhone(string phone, int id)
        {
            bool exists = _context.Users.Any(u => u.Phone == phone && u.Id != id);
            if (exists)
            {
                return Json($"Số điện thoại '{phone}' đã được sử dụng!");
            }
            return Json(true);
        }
    }
}
