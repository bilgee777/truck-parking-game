
using UnityEngine;
using System.Collections;

namespace BilgeKorkmaz
{
    public class VehicleController : MonoBehaviour
    {
        public bool canControll;

        [Header("Wheels")]
        public WheelCollider[] Wheel_Colliders;
        public Transform[] Wheel_Transforms;

        float currentSpeed;

        [Header("Vehicle Setup")]
        public float enginePower = 1400f;
        public float brakePower = 1400f;
        public float[] gearsPower;
        public float maxSteer = 43f;
        public float maxSpeed = 74f;

        float throttleInput;
        float steerInput;
        bool handBrake;

        Vector3 velocity;
        Vector3 localVel;
        [HideInInspector] public bool reversing;

        Rigidbody rigid;

        public int numberOfGears = 10;
        int currentGear;
        float GearFactor;
        [HideInInspector] public float Revs;

        public float GearShiftDelay = 0.3f;
        public float nextGearSpeed = 300f;

        [Header("Lights")]
        public Light[] brakeLights;
        public Light[] reverseLights;

        void Start()
        {
            ApplyCharacterStats();

            StartCoroutine(GearChanging());

            if (Wheel_Colliders.Length > 0 && Wheel_Colliders[0] != null)
                Wheel_Colliders[0].attachedRigidbody.centerOfMass = Vector3.zero;

            rigid = GetComponent<Rigidbody>();

            if (rigid != null)
            {
                rigid.interpolation = RigidbodyInterpolation.Interpolate;
                rigid.centerOfMass = Vector3.zero;
            }
        }

        void Update()
        {
            if (rigid == null)
                return;

            VehicleEngine();

            currentSpeed = rigid.velocity.magnitude * 2.23693629f;

            velocity = rigid.velocity;
            localVel = transform.InverseTransformDirection(velocity);
            reversing = localVel.z < 0;

            for (int i = 0; i < Wheel_Colliders.Length; i++)
            {
                if (Wheel_Colliders[i] == null || Wheel_Transforms[i] == null)
                    continue;

                Quaternion quat;
                Vector3 position;

                Wheel_Colliders[i].GetWorldPose(out position, out quat);
                Wheel_Transforms[i].position = position;
                Wheel_Transforms[i].rotation = quat;
            }
        }

        public void VehicleEngine()
        {
            CalculateRevs();

            if (!canControll)
                return;

            if (currentSpeed >= maxSpeed)
                rigid.drag = 0.3f;
            else
                rigid.drag = 0.1f;

            float gearPower = 1f;

            if (gearsPower != null && gearsPower.Length > currentGear)
                gearPower = gearsPower[currentGear];

            Wheel_Colliders[2].motorTorque = enginePower * throttleInput * gearPower;
            Wheel_Colliders[3].motorTorque = enginePower * throttleInput * gearPower;

            Wheel_Colliders[2].motorTorque = Mathf.Clamp(Wheel_Colliders[2].motorTorque, -enginePower, enginePower);
            Wheel_Colliders[3].motorTorque = Mathf.Clamp(Wheel_Colliders[3].motorTorque, -enginePower, enginePower);

            float steerLimit = maxSteer;

            if (currentSpeed > 1f)
                steerLimit = maxSteer / (currentSpeed / 10f);

            Wheel_Colliders[0].steerAngle = Mathf.Clamp(maxSteer * steerInput, -steerLimit, steerLimit);
            Wheel_Colliders[1].steerAngle = Mathf.Clamp(maxSteer * steerInput, -steerLimit, steerLimit);

            if (handBrake)
            {
                Wheel_Colliders[2].brakeTorque = brakePower;
                Wheel_Colliders[3].brakeTorque = brakePower;

                LightIntensity(0, 1f);
                LightIntensity(1, 0);
            }
            else
            {
                if (throttleInput <= 0.07f && throttleInput >= -0.07f)
                {
                    Wheel_Colliders[0].brakeTorque = brakePower / 5f;
                    Wheel_Colliders[1].brakeTorque = brakePower / 5f;
                    Wheel_Colliders[2].brakeTorque = brakePower / 5f;
                    Wheel_Colliders[3].brakeTorque = brakePower / 5f;

                    LightIntensity(0, 0);
                    LightIntensity(1, 0);
                }
                else if (throttleInput < 0 && !reversing)
                {
                    Wheel_Colliders[0].brakeTorque = brakePower * Mathf.Abs(throttleInput);
                    Wheel_Colliders[1].brakeTorque = brakePower * Mathf.Abs(throttleInput);
                    Wheel_Colliders[2].brakeTorque = brakePower * Mathf.Abs(throttleInput / 2f);
                    Wheel_Colliders[3].brakeTorque = brakePower * Mathf.Abs(throttleInput / 2f);

                    LightIntensity(0, 1f);
                    LightIntensity(1, 0);
                }
                else if (throttleInput > 0 && reversing)
                {
                    Wheel_Colliders[0].brakeTorque = brakePower * Mathf.Abs(throttleInput);
                    Wheel_Colliders[1].brakeTorque = brakePower * Mathf.Abs(throttleInput);
                    Wheel_Colliders[2].brakeTorque = brakePower * Mathf.Abs(throttleInput / 2f);
                    Wheel_Colliders[3].brakeTorque = brakePower * Mathf.Abs(throttleInput / 2f);

                    LightIntensity(0, 1f);
                    LightIntensity(1, 0);
                }
                else
                {
                    Wheel_Colliders[0].brakeTorque = 0;
                    Wheel_Colliders[1].brakeTorque = 0;
                    Wheel_Colliders[2].brakeTorque = 0;
                    Wheel_Colliders[3].brakeTorque = 0;

                    LightIntensity(0, 0);
                    LightIntensity(1, 0);
                }
            }

            if (reversing && throttleInput < 0)
            {
                LightIntensity(0, 0);
                LightIntensity(1, 1f);
            }
        }

        public void Move(float motor, float steer, bool hand)
        {
            throttleInput = motor;
            steerInput = steer;
            handBrake = hand;
        }

        void LightIntensity(int type, float value)
        {
            Light[] targetLights = type == 0 ? brakeLights : reverseLights;

            if (targetLights == null)
                return;

            for (int i = 0; i < targetLights.Length; i++)
            {
                if (targetLights[i] != null)
                    targetLights[i].intensity = value;
            }
        }

        IEnumerator GearChanging()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.01f);

                if (!reversing)
                {
                    float f = Mathf.Abs(currentSpeed / nextGearSpeed);
                    float upGearLimit = (1f / numberOfGears) * (currentGear + 1);
                    float downGearLimit = (1f / numberOfGears) * currentGear;

                    if (currentGear > 0 && f < downGearLimit)
                    {
                        yield return new WaitForSeconds(0);
                        currentGear--;
                    }

                    if (f > upGearLimit && currentGear < numberOfGears - 1)
                    {
                        yield return new WaitForSeconds(GearShiftDelay);
                        currentGear++;
                    }
                }
                else
                {
                    currentGear = 0;
                }
            }
        }

        private static float CurveFactor(float factor)
        {
            return 1f - (1f - factor) * (1f - factor);
        }

        private static float ULerp(float from, float to, float value)
        {
            return (1f - value) * from + value * to;
        }

        private void CalculateGearFactor()
        {
            float gearInterval = 1f / numberOfGears;

            float targetGearFactor = Mathf.InverseLerp(
                gearInterval * currentGear,
                gearInterval * (currentGear + 1),
                Mathf.Abs(currentSpeed / nextGearSpeed)
            );

            GearFactor = Mathf.Lerp(GearFactor, targetGearFactor, Time.deltaTime * 5f);
        }

        private void CalculateRevs()
        {
            CalculateGearFactor();

            float gearNumFactor = currentGear / (float)numberOfGears;
            float revsRangeMin = ULerp(0f, 1f, CurveFactor(gearNumFactor));
            float revsRangeMax = ULerp(1f, 1f, gearNumFactor);

            Revs = ULerp(revsRangeMin, revsRangeMax, GearFactor);
        }

        void ApplyCharacterStats()
        {
            int characterID = PlayerPrefs.GetInt("CharacterID", 0);

            switch (characterID)
            {
                case 0: // Raven - Balanced
                    enginePower = 1400f;
                    brakePower = 1400f;
                    maxSteer = 43f;
                    maxSpeed = 74f;
                    break;

                case 1: // Jack - Steering
                    enginePower = 1300f;
                    brakePower = 1400f;
                    maxSteer = 55f;
                    maxSpeed = 70f;
                    break;

                case 2: // Sarah - Brake
                    enginePower = 1350f;
                    brakePower = 2000f;
                    maxSteer = 43f;
                    maxSpeed = 72f;
                    break;

                case 3: // Mike - Power
                    enginePower = 1800f;
                    brakePower = 1500f;
                    maxSteer = 40f;
                    maxSpeed = 76f;
                    break;

                case 4: // Emma - Speed
                    enginePower = 1500f;
                    brakePower = 1300f;
                    maxSteer = 38f;
                    maxSpeed = 90f;
                    break;
            }
        }
    }
}