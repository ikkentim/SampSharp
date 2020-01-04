// SampSharp
// Copyright 2019 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SampSharp.Entities
{
    /// <summary>
    /// Provides extended functionality for configuring a <see cref="IEcsBuilder" /> instance.
    /// </summary>
    public static class EcsBuilderEnableEventExtensions
    {
        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name);
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <typeparam name="T10">The type of parameter 10 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEcsBuilder builder,
            string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9), typeof(T10));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <typeparam name="T10">The type of parameter 10 of the callback.</typeparam>
        /// <typeparam name="T11">The type of parameter 11 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IEcsBuilder builder,
            string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <typeparam name="T10">The type of parameter 10 of the callback.</typeparam>
        /// <typeparam name="T11">The type of parameter 11 of the callback.</typeparam>
        /// <typeparam name="T12">The type of parameter 12 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <typeparam name="T10">The type of parameter 10 of the callback.</typeparam>
        /// <typeparam name="T11">The type of parameter 11 of the callback.</typeparam>
        /// <typeparam name="T12">The type of parameter 12 of the callback.</typeparam>
        /// <typeparam name="T13">The type of parameter 13 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <typeparam name="T10">The type of parameter 10 of the callback.</typeparam>
        /// <typeparam name="T11">The type of parameter 11 of the callback.</typeparam>
        /// <typeparam name="T12">The type of parameter 12 of the callback.</typeparam>
        /// <typeparam name="T13">The type of parameter 13 of the callback.</typeparam>
        /// <typeparam name="T14">The type of parameter 14 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <typeparam name="T10">The type of parameter 10 of the callback.</typeparam>
        /// <typeparam name="T11">The type of parameter 11 of the callback.</typeparam>
        /// <typeparam name="T12">The type of parameter 12 of the callback.</typeparam>
        /// <typeparam name="T13">The type of parameter 13 of the callback.</typeparam>
        /// <typeparam name="T14">The type of parameter 14 of the callback.</typeparam>
        /// <typeparam name="T15">The type of parameter 15 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14),
                typeof(T15));
        }

        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1 of the callback.</typeparam>
        /// <typeparam name="T2">The type of parameter 2 of the callback.</typeparam>
        /// <typeparam name="T3">The type of parameter 3 of the callback.</typeparam>
        /// <typeparam name="T4">The type of parameter 4 of the callback.</typeparam>
        /// <typeparam name="T5">The type of parameter 5 of the callback.</typeparam>
        /// <typeparam name="T6">The type of parameter 6 of the callback.</typeparam>
        /// <typeparam name="T7">The type of parameter 7 of the callback.</typeparam>
        /// <typeparam name="T8">The type of parameter 8 of the callback.</typeparam>
        /// <typeparam name="T9">The type of parameter 9 of the callback.</typeparam>
        /// <typeparam name="T10">The type of parameter 10 of the callback.</typeparam>
        /// <typeparam name="T11">The type of parameter 11 of the callback.</typeparam>
        /// <typeparam name="T12">The type of parameter 12 of the callback.</typeparam>
        /// <typeparam name="T13">The type of parameter 13 of the callback.</typeparam>
        /// <typeparam name="T14">The type of parameter 14 of the callback.</typeparam>
        /// <typeparam name="T15">The type of parameter 15 of the callback.</typeparam>
        /// <typeparam name="T16">The type of parameter 16 of the callback.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the callback.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            this IEcsBuilder builder, string name)
        {
            return builder.EnableEvent(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),
                typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14),
                typeof(T15), typeof(T16));
        }
    }
}