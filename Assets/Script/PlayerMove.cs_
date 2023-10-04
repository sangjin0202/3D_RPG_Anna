using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = Joystick_UGUI2.Input;

//1. VariableJoystick를 우선 순위에서 올려주세요....
//2. Input를 등록해서 사용하시기를 권함....
namespace Joystick_UGUI2
{
	public class PlayerMove : MonoBehaviour
	{
		
		Transform trans;
		Vector3 move;
		Vector3 zero = Vector3.zero;


		[SerializeField] float speed = 3f;
		[SerializeField] float speedTurn = 180f;
		eSkillNumber skillNumber;
		[SerializeField] Transform cameraRig;
		[SerializeField] Vector2 cameraRigMinMax = new Vector2(-60f, +60f);
		float angleCameraRigX;

		[SerializeField] Bullet bullet;
		[SerializeField] List<Transform> firepoint = new List<Transform>();

		void Start()
		{
			trans = transform;
			angleCameraRigX = cameraRig.localEulerAngles.x;
		}

		public void SetSkill(eSkillNumber _skill)
		{
			skillNumber = _skill;
		}

		void Update()
		{
			eSkillNumber _skill = skillNumber;
			skillNumber			= eSkillNumber.None;
			float _v	= Input.GetAxisRaw("Vertical");
			float _h	= Input.GetAxisRaw("Horizontal");
			bool _bJump = Input.GetKeyDown(KeyCode.Space);
			float _mx	= Input.GetAxis("Mouse X");
			float _my	= Input.GetAxis("Mouse Y");
			bool _press = Input.GetMouseButton(1);
			move.Set(_h, 0, _v);
			//float _


			//move
			if (move != zero)
				trans.Translate(move.normalized * speed * Time.deltaTime);
			//rotate
			if (_mx != 0f && _press)
				trans.Rotate(_mx * Vector3.up * speedTurn * Time.deltaTime);
			if (_my != 0f && _press)
			{
				angleCameraRigX -= _my * speedTurn * Time.deltaTime;
				angleCameraRigX = Mathf.Clamp(angleCameraRigX, cameraRigMinMax.x, cameraRigMinMax.y);
				cameraRig.localRotation = Quaternion.Euler(Vector3.right * angleCameraRigX);
			}

			if (_skill != eSkillNumber.None)
			{
				switch (_skill)
				{
					case eSkillNumber.Skill1:
						Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);
						break;
					case eSkillNumber.Skill2:
						Instantiate(bullet, firepoint[1].position, firepoint[1].rotation);
						Instantiate(bullet, firepoint[2].position, firepoint[2].rotation);
						break;
					case eSkillNumber.Skill3:
						Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);
						Instantiate(bullet, firepoint[1].position, firepoint[1].rotation);
						Instantiate(bullet, firepoint[2].position, firepoint[2].rotation);
						break;
					case eSkillNumber.Skill4:
						Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);
						Instantiate(bullet, firepoint[1].position, firepoint[1].rotation);
						Instantiate(bullet, firepoint[2].position, firepoint[2].rotation);
						Instantiate(bullet, firepoint[3].position, firepoint[3].rotation);
						Instantiate(bullet, firepoint[4].position, firepoint[4].rotation);
						break;
				}
			}
		}
	}
}