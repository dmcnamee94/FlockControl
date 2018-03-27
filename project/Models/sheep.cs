namespace project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Helpers;


    public class ChartViewModel
    {
        public Chart Chart { get; set; }
    }


    public partial class sheep
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SheepID { get; set; }

       
        [Required]
        [Display(Name = "Breed Type")]
        [DataType(DataType.Text)]
        public string Breed { get; set; }

        [Display(Name = "Date Of Birth")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Required")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "Tag Number")]
        [Required(ErrorMessage = "Tag No Required")]
       // [Remote("doessheeptagExist", "Sheep", HttpMethod = "POST", ErrorMessage = "Tag Number already in use. Please enter a different Tag Number")]
        public virtual int sheeptag { get; set; }

        [Required]
        [Display(Name = "Bought or Bred")]
        public string detail { get; set; }

        [Display(Name = "Join Year")]
        [Required]
        public int yearadded { get; set; }

        [Required]
        [Display(Name = "Sheep Sex")]
        public string sex { get; set; }

        [NotMapped]
        public List<breed> BreedCollection { get; set; }

        [NotMapped]
        public Chart Chart { get; set; }

        [NotMapped]
        public string SearchErrorMessage { get; set; }

        [NotMapped]
        public int TotalSheep { get; internal set; }

        [NotMapped]
        public string sheepdetail { get; internal set; }
    }

    
    public class createsheep
    {
        
        [Required]
        [Display(Name = "Breed Type")]
        [DataType(DataType.Text)]
        public string Breed { get; set; }

        [Display(Name = "Date Of Birth")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Required")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "Tag Number")]
        [Required(ErrorMessage = "Tag No Required")]
        [Remote("doessheeptagExist", "Sheep", HttpMethod = "POST", ErrorMessage = "Tag Number already in use. Please enter a different Tag Number")]
        public virtual int sheeptag { get; set; }

        [Required]
        [Display(Name = "Bought or Bred")]
        public string detail { get; set; }

        [Display(Name = "Join Year")]
        [Required]
        public int yearadded { get; set; }

        [Required]
        [Display(Name = "Sheep Sex")]
        public string sex { get; set; }

        [NotMapped]
        public List<breed> BreedCollection { get; set; }

    }


    [Table("medication")]
    public partial class medication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicationID { get; set; }

        [Required]
        [Display(Name = "Drug Given")]
        public string Name { get; set; }

        [Required]
        [StringLength(225)]
        public string Issue { get; set; }

        [Required]
        [StringLength(225)]
        public string Dosage { get; set; }

        [Display(Name = "Sheep Tag Number")]
        [Required]
        public int sheeptag { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Date Treated")]
        public DateTime Date { get; set; }

        [NotMapped]
        public List<sheep> SheepCollection { get; set; }

        [NotMapped]
        public List<drug> DrugCollection { get; set; }

        //[NotMapped]
       // public string issue { get; internal set; }

        [NotMapped]
        public int Year { get; internal set; }


    }

    [Table("calevent")]
    public partial class calevent
    {
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Start Date")]
        public DateTime Start { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? Enddate { get; set; }

        [StringLength(10)]
        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        [Key]
        public int EventID { get; set; }
    }

    [Table("usertable")]
    public partial class usertable
    {

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }

        [Display(Name = "FirstName")]
        [Required]
        [StringLength(225)]
        public string firstname { get; set; }

        [Display(Name = "SecondName")]
        [Required]
        [StringLength(225)]
        public string secondname { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(225)]
        public string Address { get; set; }

        [Required]
        [StringLength(225)]
        public string Postcode { get; set; }

        [Required]
        public int Telephone { get; set; }

        [Required]
        [StringLength(225)]
        public string Email { get; set; }

        [Required]
        [StringLength(225)]
        public string Username { get; set; }

        [Required]
        [StringLength(225)]
        [DataType (DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        public string LoginErrorMessage { get; set; }
        [NotMapped]
        public bool rememberMe { get; set; }

        [Required]
        [StringLength(225)]
        public string Role { get; set; }

        [NotMapped]
        public Chart Chart { get; set; }
    }

    [Table("drugsupplier")]
    public partial class drugsupplier
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(225)]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(225)]
        public string Address { get; set; }

        [Required]
        [StringLength(225)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(225)]
        public string Email { get; set; }
    }

    [Table("mealsupplier")]
    public partial class mealsupplier
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; } 

        [Required]
        [StringLength(225)]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(225)]
        public string Address { get; set; }

        [Required]
        [StringLength(225)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(225)]
        public string Email { get; set; }
    }

    [Table("breed")]
    public partial class breed
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int BreedID { get; set; }

        [Display(Name = "Breed")]
        [Column("Breed")]
        [StringLength(225)]
        public string Breed1 { get; set; }

        [Required]
        [StringLength(225)]
        public string Colour { get; set; }

        [Required]
        [StringLength(225)]
        public string Description { get; set; }
    }

    [Table("mealproduct")]
    public partial class mealproduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MealID { get; set; }

        [Required]
        [StringLength(225)]
        public string Name { get; set; }

        
        [DataType(DataType.Currency)]
        public System.Decimal Price { get; set; }

        [Required]
        [StringLength(225)]
        public string BagSize { get; set; }

        [Required]
        [StringLength(225)]
        public string Description { get; set; }

        [Display(Name = "Date Of Purchase")]
        [Column(TypeName = "date")]
        [Required]
        [DataType(DataType.Date)]
       public DateTime DateofPurchase { get; set; }

        [Required]
        [StringLength(225)]
        public string MealSupplier { get; set; }

        [NotMapped]
        public List<mealsupplier> SupplierCollection { get; set; }
    }


    [Table("drug")]
    public partial class drug
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DrugID { get; set; }

        [Required]
        [StringLength(225)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public System.Decimal Price { get; set; }

        [Required]
        [StringLength(225)]
        public string DrugSupplier { get; set; }

        [Required]
        [StringLength(225)]
        public string Withdrawalperiod { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime Dateofdisposal { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime PurchaseDate { get; set; }

        public int BottleSize { get; set; }

        public bool IsDisposedOf {get; set;}

        [NotMapped]
        public List<drugsupplier> DrugSupplierCollection { get; set; }


    }

    [Table("movement")]
    public partial class movement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovementID { get; set;}

        [Display(Name = "Reason of Movement")]
        [Required]
        public string Eventcode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Movement")]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Number of Animals Moved")]
        public int NoAnimals { get; set; }

        [Required]
        [Display(Name = "Type of Stock")]
        public string Description { get; set; }

        [Required]
        //[Remote("doesrefnoExist", "Movement", HttpMethod = "POST", ErrorMessage = "Reference Number already recorded for. Please enter a different Reference Number")]
        [Display(Name = "Reference Number")]
        public int RefNo { get; set; }

        [NotMapped]
        public List<_event> EventCollection { get; set; }
    }

    public class addmovement
    {
        [Display(Name = "Reason of Movement")]
        [Required]
        public string Eventcode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Movement")]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Number of Animals Moved")]
        public int NoAnimals { get; set; }

        [Required]
        [Display(Name = "Type of Stock")]
        public string Description { get; set; }

        [Required]
        [Remote("doesrefnoExist", "Movement", HttpMethod = "POST", ErrorMessage = "Reference Number already recorded for. Please enter a different Reference Number")]
        [Display(Name = "Reference Number")]
        public int RefNo { get; set; }

        [NotMapped]
        public List<_event> EventCollection { get; set; }
    }

    [Table("result")]
    public partial class result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResultID { get; set; }

        [Required]
        [StringLength(225)]
        public string Description { get; set; }
    }


    [Table("event")]
    public partial class _event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventID { get; set; }

        [Required]
        [StringLength(225)]
        public string Eventcode { get; set; }

        [Required]
        [StringLength(225)]
        public string Description { get; set; }
    }

    [Table("lamb")]
    public partial class lamb
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LambID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="Breed Type")]
        public string Breed { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

       
        public Int32? FinishWeight { get; set; }

        [Required]
        [Display(Name = "Lamb Phase")]
        public string ResultDes { get; set; }

        [Required]
        [Display(Name = "Mother Ewe")]
        public int sheeptag { get; set; }

       
        [Display(Name = "Tag Number")]
        [Required(ErrorMessage = "Tag No Required")]
        //[Remote("doesExist", "Lamb", HttpMethod = "POST", ErrorMessage = "Error")]
        public int TagNo { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [NotMapped]
        public List<breed> BreedCollection { get; set; }

        [NotMapped]
        public List<sheep> SheepCollection { get; set; }

        [NotMapped]
        public List<result> ResultCollection { get; set; }

        [NotMapped]
        public List<result> FinishCollection { get; set; }

        [NotMapped]
        public int TotalLambs { get; internal set;}

        [NotMapped]
        public int Weight { get; internal set; }

        [NotMapped]
        public int Year { get; internal set; }

        [NotMapped]
        public Chart Chart { get; set; }
    }

    public class createlamb
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Lamb Breed")]
        public string Breed { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

     
        public Int32? FinishWeight { get; set; }

        [Required]
        [Display(Name = "Lamb Phase")]
        public string ResultDes { get; set; }

        [Required]
        [Display(Name = "Mother Ewe")]
        public int sheeptag { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Lamb Tag Number")]
        [Required(ErrorMessage = "Tag No Required")]
        [Remote("doesExist", "Lamb", HttpMethod = "POST", ErrorMessage = "Tag Number already exists, please enter a different Tag Number")]
        public int TagNo { get; set; }

        [NotMapped]
        public List<breed> BreedCollection { get; set; }

        [NotMapped]
        public List<sheep> SheepCollection { get; set; }

        [NotMapped]
        public List<result> ResultCollection { get; set; }

        [NotMapped]
        public List<result> FinishCollection { get; set; }
    }

    [Table("lambmed")]
    public partial class lambmed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedID { get; set; }

        [Required]
        [StringLength(225)]
        public string Issue { get; set; }

        [Required]
        [Display(Name = "Date Treated")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Lamb Tag Number")]
        public int TagNo { get; set; }

        [Display(Name = "Drug Given")]
        [Required]
        [StringLength(225)]
        public string Name { get; set; }

        [Display(Name = "Amount Given")]
        [Required]
        [StringLength(225)]
        public string Dosage { get; set; }

        [NotMapped]
        public List<lamb> LambCollection { get; set; }

        [NotMapped]
        public List<drug> DrugCollection { get; set; }
    }
}
