using System;
using System.Diagnostics;

namespace Profiling
{
	/// <summary>
	/// Permet de mesurer des intervalles de temps
	/// Il est possibles de mesurer plusieurs intervalles consécutifs
	/// pour améliorer la précision de la mesure
	/// </summary>
	public class PerfMeasure
	{
		public PerfMeasure()
		{
			mIterationRunning = false;
			mIterationCount = 0;
			mTimingSum = 0;
			
			mSw = new Stopwatch();
		}
		
		
		/// <summary>
		/// Démarrer une nouvelle mesure
		/// </summary>
		public void StartNewIteration()
		{
			if(!mIterationRunning) {
				mIterationRunning = true;
				mSw.Start();	
			}
		}
		
		
		/// <summary>
		/// Stopper la mesure en cours et la sauvegarder
		/// pour le calcul de la moyenne des mesures
		/// </summary>
		public void StopIteration()
		{
			if(mIterationRunning) {
				mSw.Stop();
				mIterationRunning = false;
				mIterationCount++;
				mTimingSum += mSw.Elapsed.TotalMilliseconds;
				mSw.Reset();
			}
		}
		
		
		/// <summary>
		/// Récupérer la moyenne des mesures réalisées
		/// </summary>
		public Double GetAverageTiming()
		{
			return mTimingSum / mIterationCount;
		}
		
		
		/// <summary>
		/// Réinitialiser les paramètres du timer
		/// </summary>
		public void Reset()
		{
			mSw.Reset();
			mIterationRunning = false;
			mIterationCount = 0;
			mTimingSum = 0;
		}
		
		#region private members
		private Stopwatch mSw;
		private Boolean mIterationRunning;
		private UInt32 mIterationCount;
		private Double mTimingSum;
		#endregion
	}
}
