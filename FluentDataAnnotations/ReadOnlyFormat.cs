// Summary:
// File: WebApplication1/FluentDataAnnotations/ReadOnlyFormat.cs 
// Created at: 07/01/2015    15:04
// Created by: 

#region

using System;

#endregion

namespace FluentDataAnnotations
{
    public class ReadOnlyFormat
    {
        private readonly Func<bool> _isReadOnlyFunc;

        private bool? _isReadOnly;

        public ReadOnlyFormat(bool isReadOnly, bool displayAsDisabledInput)
        {
            _isReadOnly = isReadOnly;
            DisplayAsDisabledInput = displayAsDisabledInput;
        }

        public ReadOnlyFormat(Func<bool> isReadOnly, bool displayAsDisabledInput)
        {
            _isReadOnlyFunc = isReadOnly;
            DisplayAsDisabledInput = displayAsDisabledInput;
        }

        internal bool DisplayAsDisabledInput { get; private set; }

        internal bool IsReadOnly
        {
            get
            {
                if (_isReadOnly.HasValue)
                {
                    return _isReadOnly.Value;
                }

                if (_isReadOnlyFunc != null)
                {
                    _isReadOnly = _isReadOnlyFunc();
                }

                return _isReadOnly.Value;
            }
        }
    }
}