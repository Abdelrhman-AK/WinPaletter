using System;

namespace AnimatorNS
{
    /// <summary>
    /// Simplified easing functions for smooth animations
    /// </summary>
    public static class Easing
    {
        /// <summary>
        /// Applies easing to a linear progress value (0-1)
        /// </summary>
        public delegate float EasingFunction(float progress);

        /// <summary>
        /// Linear interpolation (no easing)
        /// </summary>
        public static readonly EasingFunction Linear = p => p;
        
        /// <summary>
        /// Critical damp function for smooth, natural animations
        /// </summary>
        public static readonly EasingFunction Damp = p => 1f - (float)Math.Cos(p * Math.PI / 2f);
        
        /// <summary>
        /// Cubic easing - Best for Windows 11 WinUI-like animations
        /// </summary>
        public static readonly EasingFunction CubicInOut = p => p < 0.5f ? 4f * p * p * p : 1f - (float)Math.Pow(-2f * p + 2f, 3) / 2f;

        /// <summary>
        /// Applies easing function with clamping
        /// </summary>
        public static float Apply(EasingFunction easing, float progress)
        {
            progress = Math.Max(0f, Math.Min(1f, progress));
            return easing?.Invoke(progress) ?? progress;
        }
    }

    /// <summary>
    /// Simplified easing types for animations
    /// </summary>
    public enum EasingType
    {
        Linear,
        Damp,
        CubicInOut
    }

    /// <summary>
    /// Extension methods for easing
    /// </summary>
    public static class EasingExtensions
    {
        public static Easing.EasingFunction ToEasingFunction(this EasingType type)
        {
            switch (type)
            {
                case EasingType.Linear: return Easing.Linear;
                case EasingType.Damp: return Easing.Damp;
                case EasingType.CubicInOut: return Easing.CubicInOut;
                default: return Easing.Linear;
            }
        }
    }
}
