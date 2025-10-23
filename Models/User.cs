using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserManager.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã người dùng")]
        [StringLength(20, ErrorMessage = "Mã người dùng không được vượt quá 20 ký tự")]
        [Display(Name = "Mã")]
        [Remote(action: "CheckUserCode", controller: "User", AdditionalFields = nameof(Id), ErrorMessage = "Mã người dùng đã tồn tại")]
        public string UserCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
        [Display(Name = "Ngày tháng năm sinh")]
        [CustomValidation(typeof(User), nameof(ValidateBirthDate))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        [Remote(action: "CheckEmail", controller: "User", AdditionalFields = nameof(Id), ErrorMessage = "Email đã được sử dụng")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^(?:\+84|0)(?:3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$", ErrorMessage = "Số điện thoại không hợp lệ. Vui lòng nhập đúng định dạng Việt Nam (VD: 0912345678 hoặc +84912345678)")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        [Remote(action: "CheckPhone", controller: "User", AdditionalFields = nameof(Id), ErrorMessage = "Số điện thoại đã được sử dụng")]
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }



        public static ValidationResult? ValidateBirthDate(DateTime birthDate, ValidationContext context)
        {
            if (birthDate > DateTime.Today)
                return new ValidationResult("Ngày sinh không thể ở tương lai");

            if (birthDate < DateTime.Today.AddYears(-120))
                return new ValidationResult("Ngày sinh không hợp lệ (quá xa trong quá khứ)");

            return ValidationResult.Success;
        }
    }

}
