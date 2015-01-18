// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ReadOnlyFormat.cs">
//   
// </copyright>
// <summary>
//   The read only format.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations
{
    using System;

    /// <summary>
    /// The read only format.
    /// </summary>
    public class ReadOnlyFormat
    {
        #region Fields

        /// <summary>
        /// The _is read only function.
        /// </summary>
        private readonly Func<bool> _isReadOnlyFunc;

        /// <summary>
        /// The _is read only.
        /// </summary>
        private bool? _isReadOnly;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyFormat"/> class.
        /// </summary>
        /// <param name="isReadOnly">
        /// The is read only.
        /// </param>
        /// <param name="displayAsReadOnlyInput">
        /// The display as read only input.
        /// </param>
        public ReadOnlyFormat(bool isReadOnly, bool displayAsReadOnlyInput)
        {
            this._isReadOnly = isReadOnly;
            this.DisplayAsReadOnlyInput = displayAsReadOnlyInput;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyFormat"/> class.
        /// </summary>
        /// <param name="isReadOnly">
        /// The is read only.
        /// </param>
        /// <param name="displayAsReadOnlyInput">
        /// The display as disabled input.
        /// </param>
        public ReadOnlyFormat(Func<bool> isReadOnly, bool displayAsReadOnlyInput)
        {
            this._isReadOnlyFunc = isReadOnly;
            this.DisplayAsReadOnlyInput = displayAsReadOnlyInput;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether display as read only input.
        /// </summary>
        internal bool DisplayAsReadOnlyInput { get; private set; }

        /// <summary>
        /// Gets a value indicating whether is read only.
        /// </summary>
        internal bool IsReadOnly
        {
            get
            {
                if (this._isReadOnly.HasValue)
                {
                    return this._isReadOnly.Value;
                }

                if (this._isReadOnlyFunc != null)
                {
                    this._isReadOnly = this._isReadOnlyFunc();
                }

                return this._isReadOnly.HasValue && this._isReadOnly.Value;
            }
        }

        #endregion
    }
}