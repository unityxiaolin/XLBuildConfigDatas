// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: WingBaseConfig.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from WingBaseConfig.proto</summary>
public static partial class WingBaseConfigReflection {

  #region Descriptor
  /// <summary>File descriptor for WingBaseConfig.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static WingBaseConfigReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChRXaW5nQmFzZUNvbmZpZy5wcm90byI4ChNXaW5nQmFzZUNvbmZpZ0RhdGFz",
          "EiEKCFdpbmdCYXNlGAEgAygLMg8uV2luZ0Jhc2VDb25maWcivAEKDldpbmdC",
          "YXNlQ29uZmlnEg4KBldpbmdJZBgBIAEoBRIQCghGaWdodGluZxgCIAEoBRIR",
          "CglJbWFnZU5hbWUYAyABKAkSIwoISXRlbUluZm8YBCADKAsyES5XaW5nQmFz",
          "ZUl0ZW1JbmZvEg0KBVBhcmFtGAUgAygFEhIKCldpbmdOYW1lSWQYBiABKAUS",
          "GQoDQUFBGAcgAygLMgwuV2luZ0Jhc2VBQUESEgoKSW50NjRWYWx1ZRgIIAEo",
          "AyIzChBXaW5nQmFzZUl0ZW1JbmZvEg4KBkl0ZW1JZBgBIAEoDRIPCgdJdGVt",
          "TnVtGAIgASgNIkkKC1dpbmdCYXNlQUFBEgoKAkExGAEgASgFEg4KBlVpbnRB",
          "MhgCIAEoDRIKCgJBMxgDIAEoCRISCgpSZXBlYXRlZEE0GAQgAygJYgZwcm90",
          "bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::WingBaseConfigDatas), global::WingBaseConfigDatas.Parser, new[]{ "WingBase" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::WingBaseConfig), global::WingBaseConfig.Parser, new[]{ "WingId", "Fighting", "ImageName", "ItemInfo", "Param", "WingNameId", "AAA", "Int64Value" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::WingBaseItemInfo), global::WingBaseItemInfo.Parser, new[]{ "ItemId", "ItemNum" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::WingBaseAAA), global::WingBaseAAA.Parser, new[]{ "A1", "UintA2", "A3", "RepeatedA4" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class WingBaseConfigDatas : pb::IMessage<WingBaseConfigDatas> {
  private static readonly pb::MessageParser<WingBaseConfigDatas> _parser = new pb::MessageParser<WingBaseConfigDatas>(() => new WingBaseConfigDatas());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<WingBaseConfigDatas> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::WingBaseConfigReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseConfigDatas() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseConfigDatas(WingBaseConfigDatas other) : this() {
    wingBase_ = other.wingBase_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseConfigDatas Clone() {
    return new WingBaseConfigDatas(this);
  }

  /// <summary>Field number for the "WingBase" field.</summary>
  public const int WingBaseFieldNumber = 1;
  private static readonly pb::FieldCodec<global::WingBaseConfig> _repeated_wingBase_codec
      = pb::FieldCodec.ForMessage(10, global::WingBaseConfig.Parser);
  private readonly pbc::RepeatedField<global::WingBaseConfig> wingBase_ = new pbc::RepeatedField<global::WingBaseConfig>();
  /// <summary>
  ///WingBase===>WingBaseConfig类的数组列表类
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::WingBaseConfig> WingBase {
    get { return wingBase_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as WingBaseConfigDatas);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(WingBaseConfigDatas other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if(!wingBase_.Equals(other.wingBase_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= wingBase_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    wingBase_.WriteTo(output, _repeated_wingBase_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    size += wingBase_.CalculateSize(_repeated_wingBase_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(WingBaseConfigDatas other) {
    if (other == null) {
      return;
    }
    wingBase_.Add(other.wingBase_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          wingBase_.AddEntriesFrom(input, _repeated_wingBase_codec);
          break;
        }
      }
    }
  }

}

public sealed partial class WingBaseConfig : pb::IMessage<WingBaseConfig> {
  private static readonly pb::MessageParser<WingBaseConfig> _parser = new pb::MessageParser<WingBaseConfig>(() => new WingBaseConfig());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<WingBaseConfig> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::WingBaseConfigReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseConfig() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseConfig(WingBaseConfig other) : this() {
    wingId_ = other.wingId_;
    fighting_ = other.fighting_;
    imageName_ = other.imageName_;
    itemInfo_ = other.itemInfo_.Clone();
    param_ = other.param_.Clone();
    wingNameId_ = other.wingNameId_;
    aAA_ = other.aAA_.Clone();
    int64Value_ = other.int64Value_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseConfig Clone() {
    return new WingBaseConfig(this);
  }

  /// <summary>Field number for the "WingId" field.</summary>
  public const int WingIdFieldNumber = 1;
  private int wingId_;
  /// <summary>
  ///WingId===>羽翼id
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int WingId {
    get { return wingId_; }
    set {
      wingId_ = value;
    }
  }

  /// <summary>Field number for the "Fighting" field.</summary>
  public const int FightingFieldNumber = 2;
  private int fighting_;
  /// <summary>
  ///Fighting===>战斗力
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Fighting {
    get { return fighting_; }
    set {
      fighting_ = value;
    }
  }

  /// <summary>Field number for the "ImageName" field.</summary>
  public const int ImageNameFieldNumber = 3;
  private string imageName_ = "";
  /// <summary>
  ///ImageName===>图片名
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string ImageName {
    get { return imageName_; }
    set {
      imageName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "ItemInfo" field.</summary>
  public const int ItemInfoFieldNumber = 4;
  private static readonly pb::FieldCodec<global::WingBaseItemInfo> _repeated_itemInfo_codec
      = pb::FieldCodec.ForMessage(34, global::WingBaseItemInfo.Parser);
  private readonly pbc::RepeatedField<global::WingBaseItemInfo> itemInfo_ = new pbc::RepeatedField<global::WingBaseItemInfo>();
  /// <summary>
  ///ItemInfo===>
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::WingBaseItemInfo> ItemInfo {
    get { return itemInfo_; }
  }

  /// <summary>Field number for the "Param" field.</summary>
  public const int ParamFieldNumber = 5;
  private static readonly pb::FieldCodec<int> _repeated_param_codec
      = pb::FieldCodec.ForInt32(42);
  private readonly pbc::RepeatedField<int> param_ = new pbc::RepeatedField<int>();
  /// <summary>
  ///Param===>其他参数
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<int> Param {
    get { return param_; }
  }

  /// <summary>Field number for the "WingNameId" field.</summary>
  public const int WingNameIdFieldNumber = 6;
  private int wingNameId_;
  /// <summary>
  ///WingNameId===>羽翼名称id
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int WingNameId {
    get { return wingNameId_; }
    set {
      wingNameId_ = value;
    }
  }

  /// <summary>Field number for the "AAA" field.</summary>
  public const int AAAFieldNumber = 7;
  private static readonly pb::FieldCodec<global::WingBaseAAA> _repeated_aAA_codec
      = pb::FieldCodec.ForMessage(58, global::WingBaseAAA.Parser);
  private readonly pbc::RepeatedField<global::WingBaseAAA> aAA_ = new pbc::RepeatedField<global::WingBaseAAA>();
  /// <summary>
  ///AAA===>
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::WingBaseAAA> AAA {
    get { return aAA_; }
  }

  /// <summary>Field number for the "Int64Value" field.</summary>
  public const int Int64ValueFieldNumber = 8;
  private long int64Value_;
  /// <summary>
  ///Int64Value===>Int64Value
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public long Int64Value {
    get { return int64Value_; }
    set {
      int64Value_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as WingBaseConfig);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(WingBaseConfig other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (WingId != other.WingId) return false;
    if (Fighting != other.Fighting) return false;
    if (ImageName != other.ImageName) return false;
    if(!itemInfo_.Equals(other.itemInfo_)) return false;
    if(!param_.Equals(other.param_)) return false;
    if (WingNameId != other.WingNameId) return false;
    if(!aAA_.Equals(other.aAA_)) return false;
    if (Int64Value != other.Int64Value) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (WingId != 0) hash ^= WingId.GetHashCode();
    if (Fighting != 0) hash ^= Fighting.GetHashCode();
    if (ImageName.Length != 0) hash ^= ImageName.GetHashCode();
    hash ^= itemInfo_.GetHashCode();
    hash ^= param_.GetHashCode();
    if (WingNameId != 0) hash ^= WingNameId.GetHashCode();
    hash ^= aAA_.GetHashCode();
    if (Int64Value != 0L) hash ^= Int64Value.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (WingId != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(WingId);
    }
    if (Fighting != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Fighting);
    }
    if (ImageName.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(ImageName);
    }
    itemInfo_.WriteTo(output, _repeated_itemInfo_codec);
    param_.WriteTo(output, _repeated_param_codec);
    if (WingNameId != 0) {
      output.WriteRawTag(48);
      output.WriteInt32(WingNameId);
    }
    aAA_.WriteTo(output, _repeated_aAA_codec);
    if (Int64Value != 0L) {
      output.WriteRawTag(64);
      output.WriteInt64(Int64Value);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (WingId != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(WingId);
    }
    if (Fighting != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Fighting);
    }
    if (ImageName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(ImageName);
    }
    size += itemInfo_.CalculateSize(_repeated_itemInfo_codec);
    size += param_.CalculateSize(_repeated_param_codec);
    if (WingNameId != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(WingNameId);
    }
    size += aAA_.CalculateSize(_repeated_aAA_codec);
    if (Int64Value != 0L) {
      size += 1 + pb::CodedOutputStream.ComputeInt64Size(Int64Value);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(WingBaseConfig other) {
    if (other == null) {
      return;
    }
    if (other.WingId != 0) {
      WingId = other.WingId;
    }
    if (other.Fighting != 0) {
      Fighting = other.Fighting;
    }
    if (other.ImageName.Length != 0) {
      ImageName = other.ImageName;
    }
    itemInfo_.Add(other.itemInfo_);
    param_.Add(other.param_);
    if (other.WingNameId != 0) {
      WingNameId = other.WingNameId;
    }
    aAA_.Add(other.aAA_);
    if (other.Int64Value != 0L) {
      Int64Value = other.Int64Value;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          WingId = input.ReadInt32();
          break;
        }
        case 16: {
          Fighting = input.ReadInt32();
          break;
        }
        case 26: {
          ImageName = input.ReadString();
          break;
        }
        case 34: {
          itemInfo_.AddEntriesFrom(input, _repeated_itemInfo_codec);
          break;
        }
        case 42:
        case 40: {
          param_.AddEntriesFrom(input, _repeated_param_codec);
          break;
        }
        case 48: {
          WingNameId = input.ReadInt32();
          break;
        }
        case 58: {
          aAA_.AddEntriesFrom(input, _repeated_aAA_codec);
          break;
        }
        case 64: {
          Int64Value = input.ReadInt64();
          break;
        }
      }
    }
  }

}

public sealed partial class WingBaseItemInfo : pb::IMessage<WingBaseItemInfo> {
  private static readonly pb::MessageParser<WingBaseItemInfo> _parser = new pb::MessageParser<WingBaseItemInfo>(() => new WingBaseItemInfo());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<WingBaseItemInfo> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::WingBaseConfigReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseItemInfo() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseItemInfo(WingBaseItemInfo other) : this() {
    itemId_ = other.itemId_;
    itemNum_ = other.itemNum_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseItemInfo Clone() {
    return new WingBaseItemInfo(this);
  }

  /// <summary>Field number for the "ItemId" field.</summary>
  public const int ItemIdFieldNumber = 1;
  private uint itemId_;
  /// <summary>
  ///ItemId===>初级经验丹id
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public uint ItemId {
    get { return itemId_; }
    set {
      itemId_ = value;
    }
  }

  /// <summary>Field number for the "ItemNum" field.</summary>
  public const int ItemNumFieldNumber = 2;
  private uint itemNum_;
  /// <summary>
  ///ItemNum===>初级经验丹数量
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public uint ItemNum {
    get { return itemNum_; }
    set {
      itemNum_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as WingBaseItemInfo);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(WingBaseItemInfo other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (ItemId != other.ItemId) return false;
    if (ItemNum != other.ItemNum) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (ItemId != 0) hash ^= ItemId.GetHashCode();
    if (ItemNum != 0) hash ^= ItemNum.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (ItemId != 0) {
      output.WriteRawTag(8);
      output.WriteUInt32(ItemId);
    }
    if (ItemNum != 0) {
      output.WriteRawTag(16);
      output.WriteUInt32(ItemNum);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (ItemId != 0) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(ItemId);
    }
    if (ItemNum != 0) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(ItemNum);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(WingBaseItemInfo other) {
    if (other == null) {
      return;
    }
    if (other.ItemId != 0) {
      ItemId = other.ItemId;
    }
    if (other.ItemNum != 0) {
      ItemNum = other.ItemNum;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          ItemId = input.ReadUInt32();
          break;
        }
        case 16: {
          ItemNum = input.ReadUInt32();
          break;
        }
      }
    }
  }

}

public sealed partial class WingBaseAAA : pb::IMessage<WingBaseAAA> {
  private static readonly pb::MessageParser<WingBaseAAA> _parser = new pb::MessageParser<WingBaseAAA>(() => new WingBaseAAA());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<WingBaseAAA> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::WingBaseConfigReflection.Descriptor.MessageTypes[3]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseAAA() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseAAA(WingBaseAAA other) : this() {
    a1_ = other.a1_;
    uintA2_ = other.uintA2_;
    a3_ = other.a3_;
    repeatedA4_ = other.repeatedA4_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public WingBaseAAA Clone() {
    return new WingBaseAAA(this);
  }

  /// <summary>Field number for the "A1" field.</summary>
  public const int A1FieldNumber = 1;
  private int a1_;
  /// <summary>
  ///A1===>A1
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int A1 {
    get { return a1_; }
    set {
      a1_ = value;
    }
  }

  /// <summary>Field number for the "UintA2" field.</summary>
  public const int UintA2FieldNumber = 2;
  private uint uintA2_;
  /// <summary>
  ///UintA2===>UintA2
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public uint UintA2 {
    get { return uintA2_; }
    set {
      uintA2_ = value;
    }
  }

  /// <summary>Field number for the "A3" field.</summary>
  public const int A3FieldNumber = 3;
  private string a3_ = "";
  /// <summary>
  ///A3===>A3
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string A3 {
    get { return a3_; }
    set {
      a3_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "RepeatedA4" field.</summary>
  public const int RepeatedA4FieldNumber = 4;
  private static readonly pb::FieldCodec<string> _repeated_repeatedA4_codec
      = pb::FieldCodec.ForString(34);
  private readonly pbc::RepeatedField<string> repeatedA4_ = new pbc::RepeatedField<string>();
  /// <summary>
  ///RepeatedA4===>RepeatedA4
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<string> RepeatedA4 {
    get { return repeatedA4_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as WingBaseAAA);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(WingBaseAAA other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (A1 != other.A1) return false;
    if (UintA2 != other.UintA2) return false;
    if (A3 != other.A3) return false;
    if(!repeatedA4_.Equals(other.repeatedA4_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (A1 != 0) hash ^= A1.GetHashCode();
    if (UintA2 != 0) hash ^= UintA2.GetHashCode();
    if (A3.Length != 0) hash ^= A3.GetHashCode();
    hash ^= repeatedA4_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (A1 != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(A1);
    }
    if (UintA2 != 0) {
      output.WriteRawTag(16);
      output.WriteUInt32(UintA2);
    }
    if (A3.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(A3);
    }
    repeatedA4_.WriteTo(output, _repeated_repeatedA4_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (A1 != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(A1);
    }
    if (UintA2 != 0) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(UintA2);
    }
    if (A3.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(A3);
    }
    size += repeatedA4_.CalculateSize(_repeated_repeatedA4_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(WingBaseAAA other) {
    if (other == null) {
      return;
    }
    if (other.A1 != 0) {
      A1 = other.A1;
    }
    if (other.UintA2 != 0) {
      UintA2 = other.UintA2;
    }
    if (other.A3.Length != 0) {
      A3 = other.A3;
    }
    repeatedA4_.Add(other.repeatedA4_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          A1 = input.ReadInt32();
          break;
        }
        case 16: {
          UintA2 = input.ReadUInt32();
          break;
        }
        case 26: {
          A3 = input.ReadString();
          break;
        }
        case 34: {
          repeatedA4_.AddEntriesFrom(input, _repeated_repeatedA4_codec);
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code