using Sandbox;

namespace Minigolf
{
	public class HoleEndCamera : ICamera
	{
		Vector3 Position;
		Rotation Rotation;

		Vector3 TargetPosition;
		Rotation TargetRotation;

		Vector3 HolePosition;

		float LerpSpeed => 4.0f;
		float DistanceAwayFromHole => 250.0f;

		float Rot = 0.0f;

		public HoleEndCamera( Vector3 holePosition )
		{
			HolePosition = holePosition;

			Position = CurrentView.Position;
			Rotation = CurrentView.Rotation;

			// TargetPosition = Position;
			// TargetRotation = Rotation;
		}

		public override void Build( ref CameraSetup camSetup )
		{
			Rot += RealTime.Delta * 10.0f;

			Rotation rot = Rotation.FromYaw( Rot );

			Vector3 dir = (Vector3.Up * 0.35f) + ( Vector3.Forward * rot );
			dir = dir.Normal;

			TargetPosition = HolePosition + Vector3.Up * 50.0f + dir * DistanceAwayFromHole;
			TargetRotation = Rotation.From( (-dir).EulerAngles );

			// Slerp slerp
			Position = Position.LerpTo( TargetPosition, RealTime.Delta * LerpSpeed );
			Rotation = Rotation.Slerp( Rotation, TargetRotation, RealTime.Delta * LerpSpeed );

			camSetup.Position = Position;
			camSetup.Rotation = Rotation;
		}

		public override void BuildInput( InputBuilder input ) { }
	}
}
