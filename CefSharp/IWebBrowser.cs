﻿// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp
{
    public interface IWebBrowser : IDisposable
    {
        /// <summary>
        /// Event handler for receiving Javascript console messages being sent from web pages.
        /// </summary>
        event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;

        /// <summary>
        /// Event handler for changes to the status message.
        /// </summary>
        event EventHandler<StatusMessageEventArgs> StatusMessage;

        /// <summary>
        /// Event handler that will get called when the browser begins loading a frame. Multiple frames may be loading at the same
        /// time. Sub-frames may start or continue loading after the main frame load has ended. This method may not be called for a
        /// particular frame if the load request for that frame fails. For notification of overall browser load status use
        /// OnLoadingStateChange instead.
        /// </summary>
        event EventHandler<FrameLoadStartEventArgs> FrameLoadStart;

        /// <summary>
        /// Event handler that will get called when the browser is done loading a frame. Multiple frames may be loading at the same
        /// time. Sub-frames may start or continue loading after the main frame load has ended. This method will always be called
        /// for all frames irrespective of whether the request completes successfully.
        /// </summary>
        event EventHandler<FrameLoadEndEventArgs> FrameLoadEnd;

        /// <summary>
        /// Event handler that will get called when the resource load for a navigation fails or is canceled.
        /// </summary>
        event EventHandler<LoadErrorEventArgs> LoadError;

        /// <summary>
        /// Event handler that will get called when the Loading state has changed.
        /// This event will be fired twice. Once when loading is initiated either programmatically or
        /// by user action, and once when loading is terminated due to completion, cancellation of failure. 
        /// </summary>
        event EventHandler<LoadingStateChangedEventArgs> LoadingStateChanged;

        /// <summary>
        /// Loads the specified URL.
        /// </summary>
        /// <param name="url">The URL to be loaded.</param>
        void Load(string url);

        /// <summary>
        /// Registers a Javascript object in this specific browser instance.
        /// </summary>
        /// <param name="name">The name of the object. (e.g. "foo", if you want the object to be accessible as window.foo).</param>
        /// <param name="objectToBind">The object to be made accessible to Javascript.</param>
        /// <param name="camelCaseJavascriptNames">camel case the javascript names of properties/methods, defaults to true</param>
        void RegisterJsObject(string name, object objectToBind, bool camelCaseJavascriptNames = true);

        /// <summary>
        /// Execute some Javascript code in the context of this WebBrowser, and return the result of the evaluation
        /// in an Async fashion
        /// </summary>
        /// <param name="script">The Javascript code that should be executed.</param>
        /// <param name="timeout">The timeout after which the Javascript code execution should be aborted.</param>
        /// <returns>A Task that can be awaited to perform the script execution</returns>
        Task<JavascriptResponse> EvaluateScriptAsync(string script, TimeSpan? timeout = null);

        /// <summary>
        /// Implement <see cref="IDialogHandler"/> and assign to handle dialog events.
        /// </summary>
        IDialogHandler DialogHandler { get; set; }
        
        /// <summary>
        /// Implement <see cref="IRequestHandler"/> and assign to handle events related to browser requests.
        /// </summary>
        IRequestHandler RequestHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IPopupHandler"/> and assign to handle events related to popup window events.
        /// </summary>
        IPopupHandler PopupHandler { get; set; }

        /// <summary>
        /// Implement <see cref="ILifeSpanHandler"/> and assign to handle events related to popups.
        /// </summary>
        ILifeSpanHandler LifeSpanHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IKeyboardHandler"/> and assign to handle events related to key press.
        /// </summary>
        IKeyboardHandler KeyboardHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IJsDialogHandler"/> and assign to handle events related to JavaScript Dialogs.
        /// </summary>
        IJsDialogHandler JsDialogHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IDragHandler"/> and assign to handle events related to dragging.
        /// </summary>
        IDragHandler DragHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IDownloadHandler"/> and assign to handle events related to downloading files.
        /// </summary>
        IDownloadHandler DownloadHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IMenuHandler"/> and assign to handle events related to the browser context menu
        /// </summary>
        IMenuHandler MenuHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IFocusHandler"/> and assign to handle events related to the browser component's focus
        /// </summary>
        IFocusHandler FocusHandler { get; set; }

        /// <summary>
        /// Implement <see cref="IResourceHandlerFactory"/> and control the loading of resources
        /// </summary>
        IResourceHandlerFactory ResourceHandlerFactory { get; set; }

        /// <summary>
        /// Implement <see cref="IGeolocationHandler"/> and assign to handle requests for permission to use geolocation.
        /// </summary>
        IGeolocationHandler GeolocationHandler { get; set; }

        /// <summary>
        /// A flag that indicates whether the WebBrowser is initialized (true) or not (false).
        /// </summary>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool IsBrowserInitialized { get; }

        /// <summary>
        /// A flag that indicates whether the control is currently loading one or more web pages (true) or not (false).
        /// </summary>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool IsLoading { get; }

        /// <summary>
        /// A flag that indicates whether the state of the control current supports the GoBack action (true) or not (false).
        /// </summary>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool CanGoBack { get; }

        /// <summary>
        /// A flag that indicates whether the state of the control currently supports the GoForward action (true) or not (false).
        /// </summary>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool CanGoForward { get; }

        /// <summary>
        /// A flag that indicates whether the state of the control currently supports the Reload action (true) or not (false).
        /// </summary>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool CanReload { get; }

        /// <summary>
        /// The address (URL) which the browser control is currently displaying.
        /// Will automatically be updated as the user navigates to another page (e.g. by clicking on a link).
        /// </summary>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        string Address { get; }

        /// <summary>
        /// The title of the web page being currently displayed.
        /// </summary>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        string Title { get; }

        /// <summary>
        /// The text that will be displayed as a ToolTip
        /// </summary>
        string TooltipText { get; }

        /// <summary>
        /// Asynchronously gets the current Zoom Level.
        /// </summary>
        Task<double> GetZoomLevelAsync();

        /// <summary>
        /// Change the ZoomLevel to the specified value. Can be set to 0.0 to clear the zoom level.
        /// </summary>
        /// <remarks>
        /// If called on the CEF UI thread the change will be applied immediately.
        /// Otherwise, the change will be applied asynchronously on the CEF UI thread.
        /// The CEF UI thread is different to the WPF/WinForms UI Thread
        /// </remarks>
        /// <param name="level">zoom level</param>
        void SetZoomLevel(double level);

        /// <summary>
        /// Search for text within the current page.
        /// </summary>
        /// <param name="identifier">Can be used in can conjunction with searchText to have multiple
        /// searches running simultaneously.</param>
        /// <param name="searchText">search text</param>
        /// <param name="forward">indicates whether to search forward or backward within the page.</param>
        /// <param name="matchCase">indicates whether the search should be case-sensitive. </param>
        /// <param name="findNext">indicates whether this is the first request or a follow-up.</param>
        void Find(int identifier, string searchText, bool forward, bool matchCase, bool findNext);

        /// <summary>
        /// Cancel all searches that are currently going on.
        /// </summary>
        /// <param name="clearSelection">clear the current search selection</param>
        void StopFinding(bool clearSelection);

        /// <summary>
        /// Navigates back, must check <see cref="CanGoBack"/> before calling this method.
        /// </summary>
        void Back();

        /// <summary>
        /// Navigates forward, must check <see cref="CanGoForward"/> before calling this method.
        /// </summary>
        void Forward();

        /// <summary>
        /// Stops loading the current page.
        /// </summary>
        void Stop();

        /// <summary>
        /// Attempts to give focus to the IWpfWebBrowser control.
        /// </summary>
        /// <returns><c>true</c> if keyboard focus and logical focus were set to this element; <c>false</c> if only logical focus
        /// was set to this element, or if the call to this method did not force the focus to change.</returns>
        bool Focus();

        /// <summary>
        /// Reloads the page being displayed. This method will use data from the browser's cache, if available.
        /// </summary>
        void Reload();

        /// <summary>
        /// Reloads the page being displayed, optionally ignoring the cache (which means the whole page including all .css, .js
        /// etc. resources will be re-fetched).
        /// </summary>
        /// <param name="ignoreCache"><c>true</c> A reload is performed ignoring browser cache; <c>false</c> A reload is
        /// performed using files from the browser cache, if available.</param>
        void Reload(bool ignoreCache);

        /// <summary>
        /// Opens a Print Dialog which if used (can be user cancelled) will print the browser contents.
        /// </summary>
        void Print();

        /// <summary>
        /// Open developer tools in its own window. 
        /// </summary>
        void ShowDevTools();

        /// <summary>
        /// Explicitly close the developer tools window if one exists for this browser instance.
        /// </summary>
        void CloseDevTools();   
  
        /// <summary>
        /// If a misspelled word is currently selected in an editable node calling
        /// this method will replace it with the specified word. 
        /// </summary>
        /// <param name="word">The new word that will replace the currently selected word.</param>
        void ReplaceMisspelling(string word);

        /// <summary>
        /// Add the specified word to the spelling dictionary.
        /// </summary>
        /// <param name="word">The new word that will be added to the dictionary.</param>
        void AddWordToDictionary(string word);
        
        /// <summary>
        /// Send a mouse wheel event to the browser.
        /// </summary>
        /// <param name="x">X-Axis coordinate relative to the upper-left corner of the view.</param>
        /// <param name="y">Y-Axis coordinate relative to the upper-left corner of the view.</param>
        /// <param name="deltaX">Movement delta for X direction.</param>
        /// <param name="deltaY">movement delta for Y direction.</param>
        void SendMouseWheelEvent(int x, int y, int deltaX, int deltaY);

        /// <summary>
        /// Returns the main (top-level) frame for the browser window.
        /// </summary>
        /// <returns>Frame</returns>
        IFrame GetMainFrame();

        /// <summary>
        /// Returns the focused frame for the browser window.
        /// </summary>
        /// <returns>Frame</returns>
        IFrame GetFocusedFrame();
    }
}
