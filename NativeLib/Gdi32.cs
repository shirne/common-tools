using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeLib
{
    public class Gdi32
    {


        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        //声明函数
        public static extern IntPtr CreateDC
        (
            string Driver,   //驱动名称
            string Device,   //设备名称
            string Output,   //无用，可以设定为null
            IntPtr PrintData //任意的打印机数据
         );


        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern bool BitBlt(
            IntPtr hdcDest,     //目标设备的句柄
            int XDest,          //目标对象的左上角X坐标
            int YDest,          //目标对象的左上角的Y坐标
            int Width,          //目标对象的宽度
            int Height,         //目标对象的高度
            IntPtr hdcScr,      //源设备的句柄
            int XScr,           //源设备的左上角X坐标
            int YScr,           //源设备的左上角Y坐标
            Int32 drRop         //光栅的操作值
        );
    }
}
