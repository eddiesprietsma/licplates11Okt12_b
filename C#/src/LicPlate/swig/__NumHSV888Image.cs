/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.8
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class __NumHSV888Image : Image {
  private HandleRef swigCPtr;

  internal __NumHSV888Image(IntPtr cPtr, bool cMemoryOwn) : base(VisionLabPINVOKE.__NumHSV888Image_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(__NumHSV888Image obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~__NumHSV888Image() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VisionLabPINVOKE.delete___NumHSV888Image(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public __NumHSV888Image() : this(VisionLabPINVOKE.new___NumHSV888Image__SWIG_0(), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public __NumHSV888Image(int height, int width) : this(VisionLabPINVOKE.new___NumHSV888Image__SWIG_1(height, width), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public __NumHSV888Image(HeightWidth hw) : this(VisionLabPINVOKE.new___NumHSV888Image__SWIG_2(HeightWidth.getCPtr(hw)), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public __NumHSV888Image(__NumHSV888Image image) : this(VisionLabPINVOKE.new___NumHSV888Image__SWIG_3(__NumHSV888Image.getCPtr(image)), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Clear() {
    VisionLabPINVOKE.__NumHSV888Image_Clear(swigCPtr);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void SetBuf(string buf, int height, int width, bool deleteOrg) {
    VisionLabPINVOKE.__NumHSV888Image_SetBuf__SWIG_0(swigCPtr, buf, height, width, deleteOrg);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void SetBuf(string buf, int height, int width) {
    VisionLabPINVOKE.__NumHSV888Image_SetBuf__SWIG_1(swigCPtr, buf, height, width);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Resize(HeightWidth hw) {
    VisionLabPINVOKE.__NumHSV888Image_Resize__SWIG_0(swigCPtr, HeightWidth.getCPtr(hw));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Resize(HeightWidth hw, __NumHSV888Image properties) {
    VisionLabPINVOKE.__NumHSV888Image_Resize__SWIG_1(swigCPtr, HeightWidth.getCPtr(hw), __NumHSV888Image.getCPtr(properties));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ReadAsciiFromStream(SWIGTYPE_p_std__istream arg0) {
    VisionLabPINVOKE.__NumHSV888Image_ReadAsciiFromStream(swigCPtr, SWIGTYPE_p_std__istream.getCPtr(arg0));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void WriteAsciiToStream(SWIGTYPE_p_std__ostream os) {
    VisionLabPINVOKE.__NumHSV888Image_WriteAsciiToStream(swigCPtr, SWIGTYPE_p_std__ostream.getCPtr(os));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ReadBinFromStream(SWIGTYPE_p_std__istream arg0) {
    VisionLabPINVOKE.__NumHSV888Image_ReadBinFromStream(swigCPtr, SWIGTYPE_p_std__istream.getCPtr(arg0));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void WriteBinToStream(SWIGTYPE_p_std__ostream os) {
    VisionLabPINVOKE.__NumHSV888Image_WriteBinToStream(swigCPtr, SWIGTYPE_p_std__ostream.getCPtr(os));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public HSV888Pixel GetFirstPixelPtr() {
    IntPtr cPtr = VisionLabPINVOKE.__NumHSV888Image_GetFirstPixelPtr(swigCPtr);
    HSV888Pixel ret = (cPtr == IntPtr.Zero) ? null : new HSV888Pixel(cPtr, false);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public HSV888Pixel GetLastPixelPtr() {
    IntPtr cPtr = VisionLabPINVOKE.__NumHSV888Image_GetLastPixelPtr(swigCPtr);
    HSV888Pixel ret = (cPtr == IntPtr.Zero) ? null : new HSV888Pixel(cPtr, false);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public __NumHSV888Image Assign_Op(__NumHSV888Image image) {
    __NumHSV888Image ret = new __NumHSV888Image(VisionLabPINVOKE.__NumHSV888Image_Assign_Op__SWIG_0(swigCPtr, __NumHSV888Image.getCPtr(image)), false);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public __NumHSV888Image Assign_Op(HSV888Pixel pixel) {
    __NumHSV888Image ret = new __NumHSV888Image(VisionLabPINVOKE.__NumHSV888Image_Assign_Op__SWIG_1(swigCPtr, HSV888Pixel.getCPtr(pixel)), false);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsEqual_Op(__NumHSV888Image image) {
    bool ret = VisionLabPINVOKE.__NumHSV888Image_IsEqual_Op(swigCPtr, __NumHSV888Image.getCPtr(image));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Not(__NumHSV888Image image) {
    bool ret = VisionLabPINVOKE.__NumHSV888Image_Not(swigCPtr, __NumHSV888Image.getCPtr(image));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool CheckCoord(int x, int y) {
    bool ret = VisionLabPINVOKE.__NumHSV888Image_CheckCoord__SWIG_0(swigCPtr, x, y);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool CheckCoord(XYCoord xy) {
    bool ret = VisionLabPINVOKE.__NumHSV888Image_CheckCoord__SWIG_1(swigCPtr, XYCoord.getCPtr(xy));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public HSV888Pixel GetPixelPtr(int x, int y) {
    IntPtr cPtr = VisionLabPINVOKE.__NumHSV888Image_GetPixelPtr__SWIG_0(swigCPtr, x, y);
    HSV888Pixel ret = (cPtr == IntPtr.Zero) ? null : new HSV888Pixel(cPtr, false);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public HSV888Pixel GetPixelPtr(XYCoord xy) {
    IntPtr cPtr = VisionLabPINVOKE.__NumHSV888Image_GetPixelPtr__SWIG_1(swigCPtr, XYCoord.getCPtr(xy));
    HSV888Pixel ret = (cPtr == IntPtr.Zero) ? null : new HSV888Pixel(cPtr, false);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public XYCoord GetXYCoord(HSV888Pixel ptr) {
    XYCoord ret = new XYCoord(VisionLabPINVOKE.__NumHSV888Image_GetXYCoord(swigCPtr, HSV888Pixel.getCPtr(ptr)), true);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPixel(int x, int y, HSV888Pixel value) {
    VisionLabPINVOKE.__NumHSV888Image_SetPixel(swigCPtr, x, y, HSV888Pixel.getCPtr(value));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

}
