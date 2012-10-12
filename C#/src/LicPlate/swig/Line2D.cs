/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.8
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class Line2D : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Line2D(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Line2D obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~Line2D() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VisionLabPINVOKE.delete_Line2D(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Line2D() : this(VisionLabPINVOKE.new_Line2D__SWIG_0(), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public Line2D(Coord2D s, Coord2D e) : this(VisionLabPINVOKE.new_Line2D__SWIG_1(Coord2D.getCPtr(s), Coord2D.getCPtr(e)), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public double Length() {
    double ret = VisionLabPINVOKE.Line2D_Length(swigCPtr);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double Orientation() {
    double ret = VisionLabPINVOKE.Line2D_Orientation(swigCPtr);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Coord2D start {
    set {
      VisionLabPINVOKE.Line2D_start_set(swigCPtr, Coord2D.getCPtr(value));
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      IntPtr cPtr = VisionLabPINVOKE.Line2D_start_get(swigCPtr);
      Coord2D ret = (cPtr == IntPtr.Zero) ? null : new Coord2D(cPtr, false);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public Coord2D end {
    set {
      VisionLabPINVOKE.Line2D_end_set(swigCPtr, Coord2D.getCPtr(value));
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      IntPtr cPtr = VisionLabPINVOKE.Line2D_end_get(swigCPtr);
      Coord2D ret = (cPtr == IntPtr.Zero) ? null : new Coord2D(cPtr, false);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

}
