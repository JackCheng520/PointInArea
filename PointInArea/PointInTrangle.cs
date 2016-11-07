using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：判断某点在三角形中  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/4 14:33:11
// ================================
namespace Assets.JackCheng.PointInArea
{
    public class PointInTrangle
    {
        public static bool IsPointInTrangle(Vector3 tranglePoint1, Vector3 tranglePoint2, Vector3 tranglePoint3, Vector3 point) 
        {
            float originArea = GetTrangleArea(tranglePoint1, tranglePoint2, tranglePoint3);
            float targetArea = GetTrangleArea(tranglePoint1, tranglePoint2, point) +
                               GetTrangleArea(tranglePoint1, tranglePoint3, point) +
                               GetTrangleArea(tranglePoint2, tranglePoint3, point);
            Debug.Log("targetArea"+targetArea + "originArea:" + originArea);
            return targetArea <= originArea;
        }

        /// <summary>
        /// 获得三角形面积
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private static float GetTrangleArea(Vector3 p1,Vector3 p2,Vector3 p3) 
        {
            Vector3 v1 = p2 - p1;
            Vector3 v2 = p3 - p1;

            return Math.Abs(v2.x * v1.z - v1.x * v2.z);
        }
    }
}
