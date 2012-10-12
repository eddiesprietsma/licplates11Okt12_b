/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.8
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class BPN_ImageClassifier_HSV16 : BPN_Classifier {
  private HandleRef swigCPtr;

  internal BPN_ImageClassifier_HSV16(IntPtr cPtr, bool cMemoryOwn) : base(VisionLabPINVOKE.BPN_ImageClassifier_HSV16_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(BPN_ImageClassifier_HSV16 obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~BPN_ImageClassifier_HSV16() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VisionLabPINVOKE.delete_BPN_ImageClassifier_HSV16(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public BPN_ImageClassifier_HSV16() : this(VisionLabPINVOKE.new_BPN_ImageClassifier_HSV16__SWIG_0(), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public BPN_ImageClassifier_HSV16(int nrPixels, int nrHiddens1, int nrHiddens2, int nrClasses, BiasNodes bias, double minInput, double maxInput) : this(VisionLabPINVOKE.new_BPN_ImageClassifier_HSV16__SWIG_1(nrPixels, nrHiddens1, nrHiddens2, nrClasses, (int)bias, minInput, maxInput), true) {
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual string GetImageType() {
    string ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_GetImageType(swigCPtr);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double TrainImage(double learnRate, double momentum, HSV161616Image image, int classNr) {
    double ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_TrainImage(swigCPtr, learnRate, momentum, HSV161616Image.getCPtr(image), classNr);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double TrainImageSet(int nrOfEpochs, double learnRate, double momentum, vector_HSV161616Image images, vector_int classes, ref double meanError) {
    double ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_TrainImageSet(swigCPtr, nrOfEpochs, learnRate, momentum, vector_HSV161616Image.getCPtr(images), vector_int.getCPtr(classes), ref meanError);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double TrainClassImageSet(int nrOfEpochs, double learnRate, double momentum, ClassImageSet_HSV16 cis, ref double meanError) {
    double ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_TrainClassImageSet(swigCPtr, nrOfEpochs, learnRate, momentum, ClassImageSet_HSV16.getCPtr(cis), ref meanError);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int Classify(HSV161616Image image, ref double confidency) {
    int ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_Classify(swigCPtr, HSV161616Image.getCPtr(image), ref confidency);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double ClassifyOutputTab(HSV161616Image image, vector_ClassOutput outputTab) {
    double ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_ClassifyOutputTab(swigCPtr, HSV161616Image.getCPtr(image), vector_ClassOutput.getCPtr(outputTab));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double EvaluateImage(HSV161616Image image, int classExp, ref int classRes, ref double confidency, vector_double output) {
    double ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_EvaluateImage(swigCPtr, HSV161616Image.getCPtr(image), classExp, ref classRes, ref confidency, vector_double.getCPtr(output));
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double EvaluateImageSet(vector_HSV161616Image images, vector_int classesExp, vector_int classesRes, vector_double confidencies, vector_vector_double outputs, ref double meanError) {
    double ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_EvaluateImageSet(swigCPtr, vector_HSV161616Image.getCPtr(images), vector_int.getCPtr(classesExp), vector_int.getCPtr(classesRes), vector_double.getCPtr(confidencies), vector_vector_double.getCPtr(outputs), ref meanError);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double EvaluateClassImageSet(ClassImageSet_HSV16 cis, vector_int classesRes, vector_double confidencies, vector_vector_double outputs, ref double meanError) {
    double ret = VisionLabPINVOKE.BPN_ImageClassifier_HSV16_EvaluateClassImageSet(swigCPtr, ClassImageSet_HSV16.getCPtr(cis), vector_int.getCPtr(classesRes), vector_double.getCPtr(confidencies), vector_vector_double.getCPtr(outputs), ref meanError);
    if (VisionLabPINVOKE.SWIGPendingException.Pending) throw VisionLabPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
