/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.8
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class BlobAnalyseModifier : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal BlobAnalyseModifier(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(BlobAnalyseModifier obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~BlobAnalyseModifier() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VisionLabPINVOKE.delete_BlobAnalyseModifier(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public BlobAnalyseModifier(int bEMED, int aPMD, int mMC) : this(VisionLabPINVOKE.new_BlobAnalyseModifier__SWIG_0(bEMED, aPMD, mMC), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public BlobAnalyseModifier(int bEMED, int aPMD) : this(VisionLabPINVOKE.new_BlobAnalyseModifier__SWIG_1(bEMED, aPMD), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public BlobAnalyseModifier(int bEMED) : this(VisionLabPINVOKE.new_BlobAnalyseModifier__SWIG_2(bEMED), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public BlobAnalyseModifier() : this(VisionLabPINVOKE.new_BlobAnalyseModifier__SWIG_3(), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public int bendingEnergyMaxEdgeDiff {
    set {
      VisionLabPINVOKE.BlobAnalyseModifier_bendingEnergyMaxEdgeDiff_set(swigCPtr, value);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = VisionLabPINVOKE.BlobAnalyseModifier_bendingEnergyMaxEdgeDiff_get(swigCPtr);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int approxPolygonMinDistance {
    set {
      VisionLabPINVOKE.BlobAnalyseModifier_approxPolygonMinDistance_set(swigCPtr, value);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = VisionLabPINVOKE.BlobAnalyseModifier_approxPolygonMinDistance_get(swigCPtr);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int minMinCord {
    set {
      VisionLabPINVOKE.BlobAnalyseModifier_minMinCord_set(swigCPtr, value);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = VisionLabPINVOKE.BlobAnalyseModifier_minMinCord_get(swigCPtr);
      if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

}
