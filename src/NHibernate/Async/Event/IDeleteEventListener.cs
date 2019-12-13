﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;

namespace NHibernate.Event
{
	using System.Threading.Tasks;
	using System.Threading;
	public partial interface IDeleteEventListener
	{
		/// <summary>Handle the given delete event. </summary>
		/// <param name="event">The delete event to be handled. </param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		Task OnDeleteAsync(DeleteEvent @event, CancellationToken cancellationToken);

		Task OnDeleteAsync(DeleteEvent @event, ISet<object> transientEntities, CancellationToken cancellationToken);
	}
}