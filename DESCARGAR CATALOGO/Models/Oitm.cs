using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DESCARGAR_CATALOGO.Models;

[Table("OITM")]
[Index("CommisGrp", Name = "OITM_COM_GROUP")]
[Index("InvntItem", Name = "OITM_INVENTORY")]
[Index("ItemName", Name = "OITM_ITEM_NAME")]
[Index("PrchseItem", Name = "OITM_PURCHASE")]
[Index("SellItem", Name = "OITM_SALE")]
[Index("TreeType", Name = "OITM_TREE_TYPE")]
public partial class Oitm
{
    [Key]
    [StringLength(50)]
    public string ItemCode { get; set; } = null!;

    [StringLength(200)]
    public string? ItemName { get; set; }

    [StringLength(200)]
    public string? FrgnName { get; set; }

    public short? ItmsGrpCod { get; set; }

    public short? CstGrpCode { get; set; }

    [StringLength(8)]
    public string? VatGourpSa { get; set; }

    [StringLength(254)]
    public string? CodeBars { get; set; }

    [Column("VATLiable")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Vatliable { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? PrchseItem { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? SellItem { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? InvntItem { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OnHand { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? IsCommited { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OnOrder { get; set; }

    [StringLength(15)]
    public string? IncomeAcct { get; set; }

    [StringLength(15)]
    public string? ExmptIncom { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? MaxLevel { get; set; }

    [Column("DfltWH")]
    [StringLength(8)]
    public string? DfltWh { get; set; }

    [StringLength(15)]
    public string? CardCode { get; set; }

    [StringLength(50)]
    public string? SuppCatNum { get; set; }

    [StringLength(100)]
    public string? BuyUnitMsr { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? NumInBuy { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? ReorderQty { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? MinLevel { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? LstEvlPric { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LstEvlDate { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? CustomPer { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Canceled { get; set; }

    public int? MnufctTime { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? WholSlsTax { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? RetilrTax { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? SpcialDisc { get; set; }

    public short? DscountCod { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? TrackSales { get; set; }

    [StringLength(100)]
    public string? SalUnitMsr { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? NumInSale { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Consig { get; set; }

    public int? QueryGroup { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Counted { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OpenBlnc { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? EvalSystem { get; set; }

    public short? UserSign { get; set; }

    [Column("FREE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Free { get; set; }

    [StringLength(200)]
    public string? PicturName { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Transfered { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? BlncTrnsfr { get; set; }

    [Column(TypeName = "ntext")]
    public string? UserText { get; set; }

    [StringLength(17)]
    public string? SerialNum { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? CommisPcnt { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? CommisSum { get; set; }

    public short? CommisGrp { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? TreeType { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? TreeQty { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? LastPurPrc { get; set; }

    [StringLength(3)]
    public string? LastPurCur { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastPurDat { get; set; }

    [StringLength(3)]
    public string? ExitCur { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? ExitPrice { get; set; }

    [Column("ExitWH")]
    [StringLength(8)]
    public string? ExitWh { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? AssetItem { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? WasCounted { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ManSerNum { get; set; }

    [Column("SHeight1", TypeName = "numeric(19, 6)")]
    public decimal? Sheight1 { get; set; }

    [Column("SHght1Unit")]
    public short? Shght1Unit { get; set; }

    [Column("SHeight2", TypeName = "numeric(19, 6)")]
    public decimal? Sheight2 { get; set; }

    [Column("SHght2Unit")]
    public short? Shght2Unit { get; set; }

    [Column("SWidth1", TypeName = "numeric(19, 6)")]
    public decimal? Swidth1 { get; set; }

    [Column("SWdth1Unit")]
    public short? Swdth1Unit { get; set; }

    [Column("SWidth2", TypeName = "numeric(19, 6)")]
    public decimal? Swidth2 { get; set; }

    [Column("SWdth2Unit")]
    public short? Swdth2Unit { get; set; }

    [Column("SLength1", TypeName = "numeric(19, 6)")]
    public decimal? Slength1 { get; set; }

    [Column("SLen1Unit")]
    public short? Slen1Unit { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Slength2 { get; set; }

    [Column("SLen2Unit")]
    public short? Slen2Unit { get; set; }

    [Column("SVolume", TypeName = "numeric(19, 6)")]
    public decimal? Svolume { get; set; }

    [Column("SVolUnit")]
    public short? SvolUnit { get; set; }

    [Column("SWeight1", TypeName = "numeric(19, 6)")]
    public decimal? Sweight1 { get; set; }

    [Column("SWght1Unit")]
    public short? Swght1Unit { get; set; }

    [Column("SWeight2", TypeName = "numeric(19, 6)")]
    public decimal? Sweight2 { get; set; }

    [Column("SWght2Unit")]
    public short? Swght2Unit { get; set; }

    [Column("BHeight1", TypeName = "numeric(19, 6)")]
    public decimal? Bheight1 { get; set; }

    [Column("BHght1Unit")]
    public short? Bhght1Unit { get; set; }

    [Column("BHeight2", TypeName = "numeric(19, 6)")]
    public decimal? Bheight2 { get; set; }

    [Column("BHght2Unit")]
    public short? Bhght2Unit { get; set; }

    [Column("BWidth1", TypeName = "numeric(19, 6)")]
    public decimal? Bwidth1 { get; set; }

    [Column("BWdth1Unit")]
    public short? Bwdth1Unit { get; set; }

    [Column("BWidth2", TypeName = "numeric(19, 6)")]
    public decimal? Bwidth2 { get; set; }

    [Column("BWdth2Unit")]
    public short? Bwdth2Unit { get; set; }

    [Column("BLength1", TypeName = "numeric(19, 6)")]
    public decimal? Blength1 { get; set; }

    [Column("BLen1Unit")]
    public short? Blen1Unit { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? Blength2 { get; set; }

    [Column("BLen2Unit")]
    public short? Blen2Unit { get; set; }

    [Column("BVolume", TypeName = "numeric(19, 6)")]
    public decimal? Bvolume { get; set; }

    [Column("BVolUnit")]
    public short? BvolUnit { get; set; }

    [Column("BWeight1", TypeName = "numeric(19, 6)")]
    public decimal? Bweight1 { get; set; }

    [Column("BWght1Unit")]
    public short? Bwght1Unit { get; set; }

    [Column("BWeight2", TypeName = "numeric(19, 6)")]
    public decimal? Bweight2 { get; set; }

    [Column("BWght2Unit")]
    public short? Bwght2Unit { get; set; }

    [StringLength(3)]
    public string? FixCurrCms { get; set; }

    public short? FirmCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LstSalDate { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup1 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup2 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup3 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup4 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup5 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup6 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup7 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup8 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup9 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup10 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup11 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup12 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup13 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup14 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup15 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup16 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup17 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup18 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup19 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup20 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup21 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup22 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup23 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup24 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup25 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup26 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup27 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup28 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup29 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup30 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup31 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup32 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup33 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup34 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup35 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup36 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup37 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup38 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup39 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup40 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup41 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup42 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup43 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup44 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup45 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup46 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup47 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup48 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup49 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup50 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup51 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup52 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup53 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup54 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup55 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup56 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup57 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup58 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup59 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup60 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup61 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup62 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup63 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QryGroup64 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(20)]
    public string? ExportCode { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? SalFactor1 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? SalFactor2 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? SalFactor3 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? SalFactor4 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PurFactor1 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PurFactor2 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PurFactor3 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PurFactor4 { get; set; }

    [StringLength(40)]
    public string? SalFormula { get; set; }

    [StringLength(40)]
    public string? PurFormula { get; set; }

    [StringLength(8)]
    public string? VatGroupPu { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? AvgPrice { get; set; }

    [StringLength(30)]
    public string? PurPackMsr { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PurPackUn { get; set; }

    [StringLength(30)]
    public string? SalPackMsr { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? SalPackUn { get; set; }

    [Column("SCNCounter")]
    public short? Scncounter { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ManBtchNum { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ManOutOnly { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? DataSource { get; set; }

    [Column("validFor")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ValidFor { get; set; }

    [Column("validFrom", TypeName = "datetime")]
    public DateTime? ValidFrom { get; set; }

    [Column("validTo", TypeName = "datetime")]
    public DateTime? ValidTo { get; set; }

    [Column("frozenFor")]
    [StringLength(1)]
    [Unicode(false)]
    public string? FrozenFor { get; set; }

    [Column("frozenFrom", TypeName = "datetime")]
    public DateTime? FrozenFrom { get; set; }

    [Column("frozenTo", TypeName = "datetime")]
    public DateTime? FrozenTo { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? BlockOut { get; set; }

    [StringLength(30)]
    public string? ValidComm { get; set; }

    [StringLength(30)]
    public string? FrozenComm { get; set; }

    public int? LogInstanc { get; set; }

    [StringLength(20)]
    public string? ObjType { get; set; }

    [Column("SWW")]
    [StringLength(16)]
    public string? Sww { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Deleted { get; set; }

    public int? DocEntry { get; set; }

    [StringLength(15)]
    public string? ExpensAcct { get; set; }

    [StringLength(15)]
    public string? FrgnInAcct { get; set; }

    public short? ShipType { get; set; }

    [Column("GLMethod")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Glmethod { get; set; }

    [Column("ECInAcct")]
    [StringLength(15)]
    public string? EcinAcct { get; set; }

    [StringLength(15)]
    public string? FrgnExpAcc { get; set; }

    [Column("ECExpAcc")]
    [StringLength(15)]
    public string? EcexpAcc { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? TaxType { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ByWh { get; set; }

    [Column("WTLiable")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Wtliable { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ItemType { get; set; }

    [StringLength(20)]
    public string? WarrntTmpl { get; set; }

    [StringLength(20)]
    public string? BaseUnit { get; set; }

    [StringLength(3)]
    public string? CountryOrg { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? StockValue { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Phantom { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? IssueMthd { get; set; }

    [Column("FREE1")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Free1 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PricingPrc { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? MngMethod { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? ReorderPnt { get; set; }

    [StringLength(100)]
    public string? InvntryUom { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? PlaningSys { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? PrcrmntMtd { get; set; }

    public short? OrdrIntrvl { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OrdrMulti { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? MinOrdrQty { get; set; }

    public int? LeadTime { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? IndirctTax { get; set; }

    [Column("TaxCodeAR")]
    [StringLength(8)]
    public string? TaxCodeAr { get; set; }

    [Column("TaxCodeAP")]
    [StringLength(8)]
    public string? TaxCodeAp { get; set; }

    [Column("OSvcCode")]
    public int? OsvcCode { get; set; }

    [Column("ISvcCode")]
    public int? IsvcCode { get; set; }

    public int? ServiceGrp { get; set; }

    [Column("NCMCode")]
    public int? Ncmcode { get; set; }

    [StringLength(3)]
    public string? MatType { get; set; }

    public int? MatGrp { get; set; }

    [StringLength(2)]
    public string? ProductSrc { get; set; }

    public int? ServiceCtg { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ItemClass { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Excisable { get; set; }

    [Column("ChapterID")]
    public int? ChapterId { get; set; }

    [Column("NotifyASN")]
    [StringLength(40)]
    public string? NotifyAsn { get; set; }

    [StringLength(20)]
    public string? ProAssNum { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? AssblValue { get; set; }

    [Column("DNFEntry")]
    public int? Dnfentry { get; set; }

    public short? UserSign2 { get; set; }

    [StringLength(30)]
    public string? Spec { get; set; }

    [StringLength(4)]
    public string? TaxCtg { get; set; }

    public int? Series { get; set; }

    public int? Number { get; set; }

    public int? FuelCode { get; set; }

    [StringLength(2)]
    public string? BeverTblC { get; set; }

    [StringLength(2)]
    public string? BeverGrpC { get; set; }

    [Column("BeverTM")]
    public int? BeverTm { get; set; }

    [Column(TypeName = "ntext")]
    public string? Attachment { get; set; }

    public int? AtcEntry { get; set; }

    public int? ToleranDay { get; set; }

    public int? UgpEntry { get; set; }

    [Column("PUoMEntry")]
    public int? PuoMentry { get; set; }

    [Column("SUoMEntry")]
    public int? SuoMentry { get; set; }

    [Column("IUoMEntry")]
    public int? IuoMentry { get; set; }

    public short? IssuePriBy { get; set; }

    [StringLength(20)]
    public string? AssetClass { get; set; }

    [StringLength(15)]
    public string? AssetGroup { get; set; }

    [StringLength(12)]
    public string? InventryNo { get; set; }

    public int? Technician { get; set; }

    public int? Employee { get; set; }

    public int? Location { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? StatAsset { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Cession { get; set; }

    [Column("DeacAftUL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? DeacAftUl { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? AsstStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CapDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AcqDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RetDate { get; set; }

    [Column("GLPickMeth")]
    [StringLength(1)]
    [Unicode(false)]
    public string? GlpickMeth { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? NoDiscount { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? MgrByQty { get; set; }

    [StringLength(100)]
    public string? AssetRmk1 { get; set; }

    [StringLength(100)]
    public string? AssetRmk2 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? AssetAmnt1 { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? AssetAmnt2 { get; set; }

    [StringLength(15)]
    public string? DeprGroup { get; set; }

    [StringLength(32)]
    public string? AssetSerNo { get; set; }

    [StringLength(100)]
    public string? CntUnitMsr { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? NumInCnt { get; set; }

    [Column("INUoMEntry")]
    public int? InuoMentry { get; set; }

    [Column("OneBOneRec")]
    [StringLength(1)]
    [Unicode(false)]
    public string? OneBoneRec { get; set; }

    [StringLength(2)]
    public string? RuleCode { get; set; }

    [StringLength(10)]
    public string? ScsCode { get; set; }

    [StringLength(2)]
    public string? SpProdType { get; set; }

    [Column("IWeight1", TypeName = "numeric(19, 6)")]
    public decimal? Iweight1 { get; set; }

    [Column("IWght1Unit")]
    public short? Iwght1Unit { get; set; }

    [Column("IWeight2", TypeName = "numeric(19, 6)")]
    public decimal? Iweight2 { get; set; }

    [Column("IWght2Unit")]
    public short? Iwght2Unit { get; set; }

    [Column("CompoWH")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CompoWh { get; set; }

    [Column("CreateTS")]
    public int? CreateTs { get; set; }

    [Column("UpdateTS")]
    public int? UpdateTs { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? VirtAstItm { get; set; }

    [StringLength(50)]
    public string? SouVirAsst { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? InCostRoll { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PrdStdCst { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? EnAstSeri { get; set; }

    [StringLength(50)]
    public string? LinkRsc { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? OnHldPert { get; set; }

    [Column("onHldLimt", TypeName = "numeric(19, 6)")]
    public decimal? OnHldLimt { get; set; }

    public int? PriceUnit { get; set; }

    [Column("GSTRelevnt")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Gstrelevnt { get; set; }

    [Column("SACEntry")]
    public int? Sacentry { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? GstTaxCtg { get; set; }

    [Column("AssVal4WTR", TypeName = "numeric(19, 6)")]
    public decimal? AssVal4Wtr { get; set; }

    [Column("ExcImpQUoM")]
    public int? ExcImpQuoM { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? ExcFixAmnt { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? ExcRate { get; set; }

    [Column("SOIExc")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Soiexc { get; set; }

    [Column("TNVED")]
    [StringLength(10)]
    public string? Tnved { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Imported { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? AutoBatch { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? CstmActing { get; set; }

    public int? StdItemId { get; set; }

    public int? CommClass { get; set; }

    [StringLength(50)]
    public string? TaxCatCode { get; set; }

    public int? DataVers { get; set; }

    [Column("NVECode")]
    [StringLength(6)]
    public string? Nvecode { get; set; }

    [Column("CESTCode")]
    public int? Cestcode { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? CtrSealQty { get; set; }

    [StringLength(250)]
    public string? LegalText { get; set; }

    [Column("QRCodeSrc", TypeName = "ntext")]
    public string? QrcodeSrc { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Traceable { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? IfrsPsRev { get; set; }

    [StringLength(2)]
    public string? PlPaTaxCat { get; set; }

    [Column(TypeName = "numeric(19, 6)")]
    public decimal? PlPaWght { get; set; }

    [Column("PPTExReSa")]
    [StringLength(2)]
    public string? PptexReSa { get; set; }

    [Column("PPTExRePr")]
    [StringLength(2)]
    public string? PptexRePr { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ProdctType { get; set; }

    [Column("U_Currency")]
    [StringLength(3)]
    public string? UCurrency { get; set; }

    [Column("U_Origin")]
    [StringLength(1)]
    [Unicode(false)]
    public string? UOrigin { get; set; }

    [Column("U_CapWalmart")]
    public int? UCapWalmart { get; set; }

    [Column("U_CapUnimarc")]
    public int? UCapUnimarc { get; set; }

    [Column("U_FAMILIA")]
    [StringLength(50)]
    public string? UFamilia { get; set; }

    [Column("U_Sub_Familia")]
    [StringLength(50)]
    public string? USubFamilia { get; set; }

    [Column("U_MARCA")]
    [StringLength(50)]
    public string? UMarca { get; set; }

    [Column("U_CapTottus")]
    [StringLength(10)]
    public string? UCapTottus { get; set; }

    [Column("U_EXTRA")]
    [StringLength(254)]
    public string? UExtra { get; set; }

    [Column("U_BLOCAD")]
    [StringLength(1)]
    [Unicode(false)]
    public string? UBlocad { get; set; }
}
