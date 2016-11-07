using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：PointInRectangle  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/4 17:04:13
// ================================
namespace Assets.JackCheng.PointInArea
{
    public class PointInRectangle
    {
        public static bool IsPointInRectangle(Vector3 rectPoint1, Vector3 rectPoint2, Vector3 rectPoint3, Vector3 rectPoint4, Vector3 targetPoint) {
            float originArea = GetTrangleArea(rectPoint1, rectPoint2, rectPoint3)+
                               GetTrangleArea(rectPoint1,rectPoint4,rectPoint3);
            float targetArea = GetTrangleArea(rectPoint1, rectPoint2, targetPoint) +
                               GetTrangleArea(rectPoint2, rectPoint3, targetPoint) +
                               GetTrangleArea(rectPoint3, rectPoint4, targetPoint) +
                               GetTrangleArea(rectPoint4, rectPoint1, targetPoint);

            return targetArea <= originArea;
        }

        /// <summary>
        /// 获得三角形面积
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private static float GetTrangleArea(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 v1 = p2 - p1;
            Vector3 v2 = p3 - p1;

            return Math.Abs(v2.x * v1.z - v1.x * v2.z);
        }
    }
}
