using System;

namespace AnimatorNS
{
    /// <summary>
    /// Optimized easing functions for smooth animations.
    /// </summary>
    public static class Easing
    {
        public delegate float EasingFunction(float progress);

        private const float PiOverTwo = (float)Math.PI / 2f;

        public static readonly EasingFunction Linear = p => p;

        /// <summary>
        /// Smooth acceleration/deceleration.
        /// </summary>
        public static readonly EasingFunction Damp = p => 1f - (float)Math.Cos(p * PiOverTwo);

        /// <summary>
        /// Fast start, smooth end (Fluent/WinUI style).
        /// </summary>
        public static readonly EasingFunction CubicInOut = p => p < 0.5f ? 4f * p * p * p : 1f - 4f * (1f - p) * (1f - p) * (1f - p);

        /// <summary>
        /// More natural smooth acceleration/deceleration.
        /// </summary>
        public static readonly EasingFunction QuinticInOut = p => p < 0.5f ? 16f * p * p * p * p * p : 1f - 16f * (1f - p) * (1f - p) * (1f - p) * (1f - p) * (1f - p);

        /// <summary>
        /// Similar to Windows animations.
        /// </summary>
        public static readonly EasingFunction FastOutSlowIn = p =>
        {
            float t = 1f - p;
            return 1f - t * t * t;
        };

        /// <summary>
        /// Smoothstep interpolation.
        /// </summary>
        public static readonly EasingFunction SmoothStep = p => p * p * (3f - 2f * p);

        /// <summary>
        /// Applies easing with optimized clamping.
        /// </summary>
        public static float Apply(EasingFunction easing, float progress)
        {
            if (progress <= 0f) return 0f;

            if (progress >= 1f) return 1f;

            return easing == null ? progress : easing(progress);
        }
    }


    public enum EasingType
    {
        Linear,
        Damp,
        CubicInOut,
        QuinticInOut,
        FastOutSlowIn,
        SmoothStep
    }


    public static class EasingExtensions
    {
        public static Easing.EasingFunction ToEasingFunction(this EasingType type)
        {
            return type switch
            {
                EasingType.Linear => Easing.Linear,
                EasingType.Damp => Easing.Damp,
                EasingType.CubicInOut => Easing.CubicInOut,
                EasingType.QuinticInOut => Easing.QuinticInOut,
                EasingType.FastOutSlowIn => Easing.FastOutSlowIn,
                EasingType.SmoothStep => Easing.SmoothStep,
                _ => Easing.QuinticInOut
            };
        }
    }
}