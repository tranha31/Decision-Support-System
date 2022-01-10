using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Core.entity
{
    /// <summary>
    /// Thông tin tỉnh
    /// </summary>
    public class Province : BaseEntity
    {
        [Column] public Guid ProvinceId { get; set; }
        [Column] public string ProvinceCode { get; set; }
        [Column] public string ProvinceName { get; set; }
        [Column] public int? BreathMachine { get; set; }
        [Column] public int? EpidemicLevel { get; set; }
        [Column] public int? HospitalBed { get; set; }
        [Column] public int? IndustrialArea { get; set; }
        [Column] public int? NumberCase { get; set; }
        [Column] public int? NumberDeath { get; set; }
        [Column] public int? NumberDoctorDispatch { get; set; }
        [Column] public int? NumberHospital { get; set; }
        [Column] public double? OneShotInjectionRate { get; set; }
        [Column] public double? TwoShotInjectionRate { get; set; }
        [Column] public int? PopulationDensity { get; set; }
        [Column] public int? TestKit { get; set; }
    }
}
