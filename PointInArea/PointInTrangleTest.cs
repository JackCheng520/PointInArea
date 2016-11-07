using Assets.JackCheng.Probe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：PointInTrangleTest  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/4 14:44:38
// ================================
namespace Assets.JackCheng.PointInArea
{
    //[ExecuteInEditMode]
    public class PointInTrangleTest : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;
        [SerializeField]
        private float fDistance = 10;
        [SerializeField]
        private Transform transHero;
        [SerializeField]
        private float fSpeed = 5;
        [SerializeField]
        private float fRotate = 20;
        [SerializeField]
        private Transform parent;

        private float fHorValue;

        private float fVerValue;

        J_Echo echoSystem = new J_Echo();

        private List<GameObject> listObjs = new List<GameObject>();

        void Start()
        {
            if (transHero == null)
                transHero = this.transform;

            for(int i = 0;i<20;i++)
            {
                echoSystem.Add(InsCube, 0.05f);
            }
        }

        private void InsCube()
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
            o.transform.SetParent(parent, false);
            o.transform.position = new Vector3( UnityEngine.Random.Range(-50,50),0.5f, UnityEngine.Random.Range(-50,50));
            o.transform.localScale = Vector3.one;
            o.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load("green") as Material;
            listObjs.Add(o);
        }
        
        void Update()
        {
            LookAt();

            fHorValue = Input.GetAxis("Horizontal");
            fVerValue = Input.GetAxis("Vertical");
            if (fHorValue != 0 && fVerValue != 0)
                SetDir();

            transHero.transform.Translate(transHero.transform.rotation * new Vector3(fHorValue * fSpeed * Time.deltaTime, 0, fVerValue * fSpeed * Time.deltaTime), Space.World);

            if (fHorValue != 0 || fVerValue != 0) {
                CheckInArea();
            }
        }

        void LateUpdate() 
        {
            echoSystem.Update();
        }
        
        void SetDir()
        {
            Vector3 dir = new Vector3(fHorValue, 0, fVerValue).normalized;
            Vector3 mDir = transHero.transform.rotation * Vector3.forward;
            float angle = Mathf.Acos(Vector3.Dot(dir, mDir)) * Mathf.Rad2Deg;
            if (angle > 0)
            {
                Vector3 tempAngles = transHero.transform.eulerAngles;
                if (fHorValue > 0)
                    transHero.transform.rotation = Quaternion.Euler(tempAngles.x, tempAngles.y + Time.deltaTime * fRotate, tempAngles.z);
                else
                    transHero.transform.rotation = Quaternion.Euler(tempAngles.x, tempAngles.y - Time.deltaTime * fRotate, tempAngles.z);
            }

            //Debug.Log("dir:" + dir + "myDir:" + mDir + "angle:" + angle);
        }

        void CheckInArea() {
            Vector3 tempAngles = transHero.transform.rotation.eulerAngles;
            Quaternion leftQua = Quaternion.Euler(tempAngles.x, tempAngles.y - 30, tempAngles.z);

            Quaternion rightQua = Quaternion.Euler(tempAngles.x, tempAngles.y + 30, tempAngles.z);
            
            for (int i = 0; i < listObjs.Count; i++) {
                if (PointInTrangle.IsPointInTrangle(transHero.transform.position,
                    transHero.position + leftQua * Vector3.forward * fDistance,
                    transHero.position + rightQua * Vector3.forward * fDistance,
                    listObjs[i].transform.position))
                {//在三角形内
                    listObjs[i].GetComponent<MeshRenderer>().sharedMaterial = Resources.Load("red") as Material;
                }
                else 
                {
                    listObjs[i].GetComponent<MeshRenderer>().sharedMaterial = Resources.Load("green") as Material;
                }
            }
        }

        void DrawArea()
        {
            Debug.DrawLine(transHero.position, transHero.position + transHero.transform.rotation * Vector3.forward * fDistance, Color.red);
            //left
            Vector3 tempAngles = transHero.transform.rotation.eulerAngles;
            Quaternion leftQua = Quaternion.Euler(tempAngles.x, tempAngles.y - 30, tempAngles.z);
            Debug.DrawLine(transHero.position, transHero.position + leftQua * Vector3.forward * fDistance, Color.red);

            //Right
            Quaternion rightQua = Quaternion.Euler(tempAngles.x, tempAngles.y + 30, tempAngles.z);
            Debug.DrawLine(transHero.position, transHero.position + rightQua * Vector3.forward * fDistance, Color.red);

            //
            Debug.DrawLine(transHero.position + leftQua * Vector3.forward * fDistance,
                           transHero.position + rightQua * Vector3.forward * fDistance,
                           Color.red);

        }

        void LookAt()
        {
            cam.transform.LookAt(this.transform);
        }

        void OnDrawGizmos()
        {
            DrawArea();
        }
    }
}
