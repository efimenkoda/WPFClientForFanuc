using FRRobot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FRRegistry;

namespace Fanuc
{
    class Program
    {
                       static void Main(string[] args)
        {
            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
            //FRCRobot robot = new FRCRobot();
            //robot.Connect("127.0.0.1");
            //Console.WriteLine(robot.IsConnected);
            //Console.WriteLine("--------------");
            //Console.ReadKey();
            ////FRCCurPosition curPositions = robot.CurPosition;
            ////FRCCurGroupPosition groupCurPosition = curPositions.Group[1,FRECurPositionConstants.frJointDisplayType];
            ////FRCXyzWpr xyzWprCur = groupCurPosition.Formats[FRETypeCodeConstants.frExtXyzWpr];
            ////Console.WriteLine("Текущие координаты");
            ////Console.WriteLine(xyzWprCur.X);
            ////Console.WriteLine(xyzWprCur.Y);
            ////Console.WriteLine(xyzWprCur.Z);

           
            

            ////FRCSysPositions sysPositions = robot.RegPositions;
            ////FRCSysPosition position = sysPositions[1];
            ////FRCSysGroupPosition groupPosition = position.Group[1];
            ////FRCXyzWpr xyzWpr = groupPosition.Formats[FRETypeCodeConstants.frXyzWpr];



            ////xyzWprCur.X = -260;
            ////xyzWprCur.Y = -520;
            ////xyzWprCur.Z = 440;
            ////xyzWprCur.W = -130;
            ////xyzWprCur.P = -50;
            ////xyzWprCur.R = -120;
            ////xyzWprCur.SetAll(-120, 20, 10, 130, 50, -120);
            ////xyzWprCur.X = -750.64;
            ////xyzWprCur.Y = 450.27;
            ////xyzWprCur.Z = 710.31;
            ////xyzWprCur.W = -100.68;
            ////xyzWprCur.P = -30.01;
            ////xyzWprCur.R = 110.05;

            ////try
            ////{
            ////    groupCurPosition.Update();
            ////    groupCurPosition.Moveto();

            ////}
            ////catch (Exception e)
            ////{
            ////    Console.WriteLine(e.ToString());
            ////}

            //FRCDigitalIOType dioType = (FRCDigitalIOType)robot.IOTypes[FREIOTypeConstants.frDInType];
            //FRCDigitalIOSignal dioSignal = (FRCDigitalIOSignal)dioType.Signals[84];
            //Console.WriteLine(dioSignal.Value);
            //dioSignal.Value = false;
            //Console.WriteLine(dioSignal.Value);

            //Console.ReadKey();
        }


        //public static void SetDigitalOutSignal(FRCRobot robot, long dioIndex, bool dioValue)
        //{
        //    try
        //    {
        //        FRCDigitalIOType dioType = (FRCDigitalIOType)robot.IOTypes[FREIOTypeConstants.frDOutType];
        //        FRCDigitalIOSignal dioSignal = (FRCDigitalIOSignal)dioType.Signals[dioIndex];
        //        Console.WriteLine(dioSignal.Value);
        //        dioSignal.Value = dioValue;
        //    }
        //    catch (Exception) { }
        //}

        //public static FRCIOSignal GetRobotIOSignal(FRCRobot robot, FREIOTypeConstants ioTypeConstant, long ioIndex)
        //{
        //    try
        //    {
        //        if (robot.IsConnected)
        //        {
        //            // Stacked case statements create the logical OR condition.
        //            // http://stackoverflow.com/questions/848472/how-add-or-in-switch-statements
        //            switch (ioTypeConstant)
        //            {
        //                case FREIOTypeConstants.frAInType:
        //                case FREIOTypeConstants.frAOutType:
        //                    ((FRCAnalogIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCAnalogIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frDInType:
        //                case FREIOTypeConstants.frDOutType:
        //                    ((FRCDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frFlagType:
        //                    ((FRCFlagType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCFlagType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frGPInType:
        //                case FREIOTypeConstants.frGPOutType:
        //                    ((FRCGroupIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCGroupIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frLAInType:
        //                case FREIOTypeConstants.frLAOutType:
        //                    return ((FRCLaserAnalogIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frLDInType:
        //                case FREIOTypeConstants.frLDOutType:
        //                    ((FRCLaserDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCLaserDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frMarkerType:
        //                    ((FRCMarkerType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCMarkerType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frMaxIOType:
        //                    return new FRCIOSignal(); // There is no FRCMaxIOType.
        //                    break;

        //                case FREIOTypeConstants.frPLCInType:
        //                case FREIOTypeConstants.frPLCOutType:
        //                    ((FRCPLCIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCPLCIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frRDInType:
        //                case FREIOTypeConstants.frRDOutType:
        //                    return new FRCIOSignal(); // There is no FRCRDIOType.
        //                    break;

        //                case FREIOTypeConstants.frSOPInType:
        //                case FREIOTypeConstants.frSOPOutType:
        //                    ((FRCSOPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCSOPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frTPInType:
        //                case FREIOTypeConstants.frTPOutType:
        //                    ((FRCTPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCTPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frUOPInType:
        //                case FREIOTypeConstants.frUOPOutType:
        //                    ((FRCUOPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCUOPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frWDInType:
        //                case FREIOTypeConstants.frWDOutType:
        //                    ((FRCWeldDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCWeldDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    break;

        //                case FREIOTypeConstants.frWSTKInType:
        //                case FREIOTypeConstants.frWSTKOutType:
        //                    ((FRCWeldStickIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex].Refresh();
        //                    return ((FRCWeldStickIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];

        //                default:
        //                    return new FRCIOSignal();
        //                    break;
        //            }
        //        }
        //        return new FRCIOSignal();
        //    }
        //    catch (Exception)
        //    {
        //        return new FRCIOSignal();
        //    }
        //}

        //public static void SetRobotIOSignal(FRCRobot robot, FREIOTypeConstants ioTypeConstant, long ioIndex, FRCIOSignal ioSignal)
        //{
        //    try
        //    {
        //        if (robot.IsConnected)
        //        {
        //            // Stacked case statements create the logical OR condition.
        //            // http://stackoverflow.com/questions/848472/how-add-or-in-switch-statements
        //            switch (ioTypeConstant)
        //            {
        //                case FREIOTypeConstants.frAInType:
        //                case FREIOTypeConstants.frAOutType:
        //                    FRCAnalogIOSignal aioSignal = (FRCAnalogIOSignal)((FRCAnalogIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    aioSignal.Comment = ((FRCAnalogIOSignal)ioSignal).Comment;
        //                    aioSignal.Value = ((FRCAnalogIOSignal)ioSignal).Value;
        //                    aioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frDInType:
        //                case FREIOTypeConstants.frDOutType:
        //                    FRCDigitalIOSignal dioSignal = (FRCDigitalIOSignal)((FRCDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    dioSignal.Comment = ((FRCDigitalIOSignal)ioSignal).Comment;
        //                    dioSignal.Value = ((FRCDigitalIOSignal)ioSignal).Value;
        //                    dioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frFlagType:
        //                    FRCFlagSignal fioSignal = (FRCFlagSignal)((FRCFlagType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    fioSignal.Comment = ((FRCFlagSignal)ioSignal).Comment;
        //                    fioSignal.Value = ((FRCFlagSignal)ioSignal).Value;
        //                    fioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frGPInType:
        //                case FREIOTypeConstants.frGPOutType:
        //                    FRCGroupIOSignal gioSignal = (FRCGroupIOSignal)((FRCGroupIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    gioSignal.Comment = ((FRCGroupIOSignal)ioSignal).Comment;
        //                    gioSignal.Value = ((FRCGroupIOSignal)ioSignal).Value;
        //                    gioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frLAInType:
        //                case FREIOTypeConstants.frLAOutType:
        //                    FRCLaserAnalogIOSignal laioSignal = (FRCLaserAnalogIOSignal)((FRCLaserAnalogIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    laioSignal.Comment = ((FRCLaserAnalogIOSignal)ioSignal).Comment;
        //                    laioSignal.Value = ((FRCLaserAnalogIOSignal)ioSignal).Value;
        //                    laioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frLDInType:
        //                case FREIOTypeConstants.frLDOutType:
        //                    FRCLaserDigitalIOSignal ldioSignal = (FRCLaserDigitalIOSignal)((FRCLaserDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    ldioSignal.Comment = ((FRCLaserDigitalIOSignal)ioSignal).Comment;
        //                    ldioSignal.Value = ((FRCLaserDigitalIOSignal)ioSignal).Value;
        //                    ldioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frMarkerType:
        //                    FRCMarkerSignal mrkioSignal = (FRCMarkerSignal)((FRCMarkerType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    mrkioSignal.Comment = ((FRCMarkerSignal)ioSignal).Comment;
        //                    mrkioSignal.Value = ((FRCMarkerSignal)ioSignal).Value;
        //                    mrkioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frMaxIOType:
        //                    // There is no FRCMaxIOType.
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frPLCInType:
        //                case FREIOTypeConstants.frPLCOutType:
        //                    FRCPLCIOSignal plcioSignal = (FRCPLCIOSignal)((FRCPLCIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    plcioSignal.Comment = ((FRCPLCIOSignal)ioSignal).Comment;
        //                    plcioSignal.Value = ((FRCPLCIOSignal)ioSignal).Value;
        //                    plcioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frRDInType:
        //                case FREIOTypeConstants.frRDOutType:
        //                    // There is no FRCRDIOType.
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frSOPInType:
        //                case FREIOTypeConstants.frSOPOutType:
        //                    FRCSOPIOSignal sopioSignal = (FRCSOPIOSignal)((FRCSOPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    sopioSignal.Comment = ((FRCSOPIOSignal)ioSignal).Comment;
        //                    sopioSignal.Value = ((FRCSOPIOSignal)ioSignal).Value;
        //                    sopioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frTPInType:
        //                case FREIOTypeConstants.frTPOutType:
        //                    FRCTPIOSignal tpioSignal = (FRCTPIOSignal)((FRCTPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    tpioSignal.Comment = ((FRCTPIOSignal)ioSignal).Comment;
        //                    tpioSignal.Value = ((FRCTPIOSignal)ioSignal).Value;
        //                    tpioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frUOPInType:
        //                case FREIOTypeConstants.frUOPOutType:
        //                    FRCUOPIOSignal uopioSignal = (FRCUOPIOSignal)((FRCUOPIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    uopioSignal.Comment = ((FRCUOPIOSignal)ioSignal).Comment;
        //                    uopioSignal.Value = ((FRCUOPIOSignal)ioSignal).Value;

        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frWDInType:
        //                case FREIOTypeConstants.frWDOutType:
        //                    FRCWeldDigitalIOSignal wdioSignal = (FRCWeldDigitalIOSignal)((FRCWeldDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    wdioSignal.Comment = ((FRCWeldDigitalIOSignal)ioSignal).Comment;
        //                    wdioSignal.Value = ((FRCWeldDigitalIOSignal)ioSignal).Value;
        //                    wdioSignal.Update();
        //                    return;
        //                    break;

        //                case FREIOTypeConstants.frWSTKInType:
        //                case FREIOTypeConstants.frWSTKOutType:
        //                    FRCWeldStickIOSignal wsioSignal = (FRCWeldStickIOSignal)((FRCWeldStickIOType)robot.IOTypes[ioTypeConstant]).Signals[ioIndex];
        //                    wsioSignal.Comment = ((FRCWeldStickIOSignal)ioSignal).Comment;
        //                    wsioSignal.Value = ((FRCWeldStickIOSignal)ioSignal).Value;
        //                    wsioSignal.Update();
        //                    return;
        //                    break;

        //                default:
        //                    return;
        //                    break;
        //            }
        //        }
        //        return;
        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }
        //}
    }
}
