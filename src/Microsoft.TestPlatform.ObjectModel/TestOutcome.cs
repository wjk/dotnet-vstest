// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel
{
    using System;

    /// <summary>
    /// Represents the outcomes of a test case.
    /// </summary>
    public enum TestOutcome
    {
        /// <summary>
        /// Test Case Does Not Have an outcome.
        /// </summary>
        None = 0,

        /// <summary>
        /// Test Case Passed
        /// </summary>
        Passed = 1,

        /// <summary>
        /// Test Case Failed
        /// </summary>
        Failed = 2,

        /// <summary>
        /// Test Case Skipped
        /// </summary>
        Skipped = 3,

        /// <summary>
        /// Test Case Not found
        /// </summary>
        NotFound = 4,
    }
}