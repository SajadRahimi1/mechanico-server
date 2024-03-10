using System.ComponentModel.DataAnnotations;

namespace Mechanico_Api.Dtos;

public class UpdateMechanicDto
{
    [Required(ErrorMessage = "نام را وارد کنید")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "نام مجموعه را وارد کنید")]
    public string StoreName { get; set; }

    [Required(ErrorMessage = "شماره تماس را وارد کنید")]
    public string CallNumber { get; set; }

    [Required(ErrorMessage = "شهر را وارد کنید")]
    public string City { get; set; }

    [Required(ErrorMessage = "خیابان را وارد کنید")]
    public string Street { get; set; }

    [Required(ErrorMessage = "توضیحات را وارد کنید")]
    public string Description { get; set; }

    [Range(double.MinValue, double.MaxValue,ErrorMessage = "latitude را وارد کنید")]
    public double Latitude { get; set; }

    [Range(double.MinValue, double.MaxValue,ErrorMessage = "longitude را وارد کنید")]
    public double Longitude { get; set; }
}