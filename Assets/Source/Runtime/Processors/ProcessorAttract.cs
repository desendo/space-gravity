//  Project : game.cryoshockmini
// Contacts : Pix - ask@pixeye.games

using Pixeye.Framework;
using UnityEngine;

namespace Space
{
	class ProcessorAttract : Processor, ITickFixed
	{
		
		public Group<ComponentObject, ComponentRigid> groupAttracts;
        public Group<ComponentObject, ComponentRigid,ComponentOrbit> groupOrbits;



        public void TickFixed(float delta)
        {
            foreach (var attracted in groupAttracts)
            {
                var cObjectAttracted = attracted.ComponentObject();
                var cRigidAttracted = attracted.ComponentRigid();
                cRigidAttracted.force = Vector3.zero;
                cRigidAttracted.orbitParent = -1;

                Vector2 maxForce = Vector2.zero;

                foreach (var attractor in groupAttracts)
                {
                    if (attractor.EqualsAndExist(attracted)) continue;
                    if (attractor.ComponentRigid().source.mass < cRigidAttracted.source.mass)
                        continue;

                    Vector2 dir = attractor.ComponentObject().position - cObjectAttracted.position;
                    float sqrMag = dir.sqrMagnitude;
                    if (sqrMag==0) continue;

                    float forceMagnitude = (cRigidAttracted.source.mass * attractor.ComponentRigid().source.mass) / sqrMag;
                    Vector2 addForce = dir.normalized * forceMagnitude;
                    cRigidAttracted.force += addForce;

                    if (attractor.Has(Tag.OrbitGiver))
                        cRigidAttracted.orbitParent = attractor;

                }

                if (attracted.transform.name == "planet")
                    Debug.Log(cRigidAttracted.force);

                cRigidAttracted.source.AddForce(cRigidAttracted.force);

                //задаем начальную скорость телу, чтоб оно по орбите шло. скорость выщитываем относительно одного родительского тела (задачу 3х тел не решаем)
                if (attracted.Has(Tag.StartOrbit) && cRigidAttracted.orbitParent != -1)
                {                    
                    Vector2 deltaR = cObjectAttracted.position - cRigidAttracted.orbitParent.ComponentObject().position;
                    Vector2 orbitVelocity = Vector2.Perpendicular(deltaR).normalized * Mathf.Sqrt(cRigidAttracted.orbitParent.ComponentRigid().source.mass / deltaR.magnitude);
                    cRigidAttracted.source.velocity = orbitVelocity;                    
                    attracted.Remove(Tag.StartOrbit);
                }
                cObjectAttracted.position = cRigidAttracted.source.position;
            }


            foreach (var orbit in groupOrbits)
            {
            

                var cOrbit = orbit.ComponentOrbit();
                var cObject = orbit.ComponentObject();
                var cRigid = orbit.ComponentRigid();

                if (cRigid.orbitParent == -1) continue;

                var cRigidParent = cRigid.orbitParent.ComponentRigid();
                var cObjectParent = cRigid.orbitParent.ComponentObject();


                
                cOrbit.orbitPoints.Clear();
                cOrbit.view.enabled = true;
                float time = 0;
                Vector2 velocity = cRigid.source.velocity;
                Vector2 position = cObject.position;
                Vector2 start_position = position;
                Vector2 orbitParentPosition = cObjectParent.position;


                Vector2 acceleration = CalculateForce(cRigid.source.mass, cRigidParent.source.mass, orbitParentPosition - position) / cRigid.source.mass;
                

                Debug.DrawLine(position, position + acceleration, Color.red);

                cOrbit.orbitPoints.Add(position);
                for (int i = 0; i < cOrbit.maxPoints; i++)
                {
                    time += cOrbit.deltaTime;
                    Vector2 new_velocity = velocity + acceleration * cOrbit.deltaTime;
                    position += (velocity + new_velocity) / 2 * cOrbit.deltaTime + acceleration * cOrbit.deltaTime * cOrbit.deltaTime / 2;


                    acceleration = CalculateForce(cRigid.source.mass, cRigidParent.source.mass, orbitParentPosition - position) / cRigid.source.mass;
                    cOrbit.orbitPoints.Add(position);
                    velocity = new_velocity;
                    if (i > 10)
                    {
                        if ((position - start_position).sqrMagnitude < 0.01f)
                            break;
                    }
                }

                Vector3[] points = new Vector3[cOrbit.orbitPoints.Count];
                cOrbit.view.positionCount = cOrbit.orbitPoints.Count;
                for (int i = 0; i < points.Length; i++)
                {

                    points[i] = new Vector3(cOrbit.orbitPoints[i].x, cOrbit.orbitPoints[i].y, 0);

                }

                cOrbit.view.SetPositions(points);

            }
        }
        public Vector2 CalculateForce(float mass1 , float mass2, Vector2 deltaPos)
        {            
            float sqrDistance = deltaPos.sqrMagnitude;
            if (sqrDistance == 0f)
                return Vector2.zero;
            float forceMagnitude = (mass1 * mass2) / sqrDistance;

            Vector2 force = deltaPos.normalized * forceMagnitude;
            return force;

        }
    }
}