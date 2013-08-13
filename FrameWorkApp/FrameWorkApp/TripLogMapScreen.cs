using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.CoreLocation;
using System.Collections.Generic;
using Google.Maps;

namespace FrameWorkApp
{
	public partial class TripLogMapScreen : UIViewController
	{
		LoadingOverlay loadingOverlay;
		Google.Maps.MapView mapView;
		private Event[] markersToAdd;
		private Boolean firstLocationUpdate;
		private CLLocationCoordinate2D[] pathMarkers;
		private List<String> googlePathStrings;
		SDMFileManager fileManager = new SDMFileManager();

		public TripLogMapScreen (IntPtr handle) : base (handle)
		{
			//From Internal Data Structures
			//markersToAdd = (CLLocationCoordinate2D[])StopScreenn.coordList.ToArray (typeof(CLLocationCoordinate2D));

			//From File

			//Temporarily Disabled....########################
			//markersToAdd = StopScreenn.fileManager.readDataFromTripEventFile ();
			//#############################
			pathMarkers = fileManager.readSavedTripDistanceData (TripLogDetailScreen.currentTrip);
			/*pathMarkers = new CLLocationCoordinate2D[StopScreen.listOfTripLocationCoordinates.Count];

			for(int i = 0; i< StopScreen.listOfTripLocationCoordinates.Count; i++){
				CLLocation newClLocation = StopScreen.listOfTripLocationCoordinates[i];
				pathMarkers[i] = new CLLocationCoordinate2D(newClLocation.Coordinate.Latitude, newClLocation.Coordinate.Longitude);
			}*/
			//From Internal Data Structures
			//markersToAdd = (CLLocationCoordinate2D[])StopScreenn.coordList.ToArray (typeof(CLLocationCoordinate2D));

			//EVENTS
			markersToAdd = fileManager.readSavedTripEventData(TripLogDetailScreen.currentTrip);
			googlePathStrings = fileManager.readSavedGooglePathData (TripLogDetailScreen.currentTrip);
		}

		public TripLogMapScreen (IntPtr handle,Event[] markerLocationsToAdd) : base (handle)
		{
			markersToAdd = markerLocationsToAdd;
		}

		//Comment
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public void addMarkerAtLocationsWithGoogleMarker(Event[] position){

			foreach(Event newEve in position){
				var newMarker = new Marker(){
					Title = newEve.Text,
					Position = newEve.Location,
					Icon = Google.Maps.Marker.MarkerImage(newEve.Color),
					Map = mapView
				};
			}
		}

		/*
		public void addMarkerAtLocationsWithCustomMarker(CLLocationCoordinate2D position, string title, string snippet, UIImage markerIcon){
			var newMarker = new Marker(){
				Title = title,
				Snippet = snippet,
				Position = position,
				Icon = markerIcon,
				Map = mapView
			};

		}
		*/

		public int getZoomLevel(double minLat, double maxLat, double minLong, double maxLong, float mapWidth, float mapHeight){
			float mapDisplayDimension = Math.Min (mapHeight, mapWidth);
			int earthRadiusinKm = 6371;
			double degToRadDivisor = 57.2958;
			double distanceToBeCovered = (earthRadiusinKm * Math.Acos (Math.Sin(minLat / degToRadDivisor) * Math.Sin(maxLat / degToRadDivisor) + 
			                                                           (Math.Cos (minLat / degToRadDivisor)  * Math.Cos (maxLat / degToRadDivisor) * Math.Cos ((maxLong / degToRadDivisor) - (minLong / degToRadDivisor)))));

			double zoomLevel = Math.Floor (8 - Math.Log(1.6446 * distanceToBeCovered / Math.Sqrt(2 * (mapDisplayDimension * mapDisplayDimension))) / Math.Log (2));
			if(minLat == maxLat && minLong == maxLong){zoomLevel = 15;}

			return (int) zoomLevel;
		}

		//Method for when we have lines on the map
		public void cameraAutoZoomAndReposition(Event[] eventMarkers, Polyline[] polylinesToPlot){
			double minimumLongitudeInGoogle = 180.0f;
			double maximumLongitudeInGoogle = -180.0f;
			double minimumLatitudeInGoogle = 90.0f;
			double maximumLatitudeInGoogle = -90.0f;
			foreach (Event currentMarker in eventMarkers) {
				maximumLongitudeInGoogle = Math.Max (maximumLongitudeInGoogle, currentMarker.Location.Longitude);
				minimumLongitudeInGoogle = Math.Min (minimumLongitudeInGoogle, currentMarker.Location.Longitude);
				maximumLatitudeInGoogle = Math.Max (maximumLatitudeInGoogle, currentMarker.Location.Latitude);
				minimumLatitudeInGoogle = Math.Min (minimumLatitudeInGoogle, currentMarker.Location.Latitude);
			}
			Console.WriteLine ("Length Path: "+polylinesToPlot.Length);
			foreach (Polyline line in polylinesToPlot) {
				uint numberOfPoints = line.Path.Count;
				Console.WriteLine ("Number of Points: "+numberOfPoints);
				for(uint i = 0; i<numberOfPoints;i++){
					//Check Start of Line
					maximumLongitudeInGoogle = Math.Max (maximumLongitudeInGoogle, line.Path.CoordinateAtIndex(i).Longitude);
					minimumLongitudeInGoogle = Math.Min (minimumLongitudeInGoogle, line.Path.CoordinateAtIndex(i).Longitude);
					maximumLatitudeInGoogle = Math.Max (maximumLatitudeInGoogle, line.Path.CoordinateAtIndex(i).Latitude);
					minimumLatitudeInGoogle = Math.Min (minimumLatitudeInGoogle, line.Path.CoordinateAtIndex(i).Latitude);
				}

			}
			Console.WriteLine ("Max Long: "+maximumLongitudeInGoogle);
			Console.WriteLine ("Min Long: "+minimumLongitudeInGoogle);
			Console.WriteLine ("Max Lat: "+maximumLatitudeInGoogle);
			Console.WriteLine ("Min Lat: "+minimumLatitudeInGoogle);

			CLLocationCoordinate2D northWestBound = new CLLocationCoordinate2D (maximumLatitudeInGoogle, minimumLongitudeInGoogle);
			CLLocationCoordinate2D southEastBound = new CLLocationCoordinate2D (minimumLatitudeInGoogle, maximumLongitudeInGoogle);

			mapView.MoveCamera(CameraUpdate.FitBounds(new CoordinateBounds(northWestBound, southEastBound)));	
			float desiredZoomlevel = (float) (getZoomLevel (minimumLatitudeInGoogle,maximumLatitudeInGoogle,minimumLongitudeInGoogle,maximumLongitudeInGoogle,mapViewOutlet.Frame.Size.Width,mapViewOutlet.Frame.Size.Height));
			desiredZoomlevel-=1;
			mapView.MoveCamera (CameraUpdate.ZoomToZoom((desiredZoomlevel)));
		}

		//Method when there are no lines to be drawn.
		public void cameraAutoZoomAndReposition(Event[] eventMarkers){
			double minimumLongitudeInGoogle = 180.0f;
			double maximumLongitudeInGoogle = -180.0f;
			double minimumLatitudeInGoogle = 90.0f;
			double maximumLatitudeInGoogle = -90.0f;
			foreach (Event currentMarker in eventMarkers) {
				maximumLongitudeInGoogle = Math.Max (maximumLongitudeInGoogle, currentMarker.Location.Longitude);
				minimumLongitudeInGoogle = Math.Min (minimumLongitudeInGoogle, currentMarker.Location.Longitude);
				maximumLatitudeInGoogle = Math.Max (maximumLatitudeInGoogle, currentMarker.Location.Latitude);
				minimumLatitudeInGoogle = Math.Min (minimumLatitudeInGoogle, currentMarker.Location.Latitude);
			}
			CLLocationCoordinate2D northWestBound = new CLLocationCoordinate2D (maximumLatitudeInGoogle, minimumLongitudeInGoogle);
			CLLocationCoordinate2D southEastBound = new CLLocationCoordinate2D (minimumLatitudeInGoogle, maximumLongitudeInGoogle);

			mapView.MoveCamera(CameraUpdate.FitBounds(new CoordinateBounds(northWestBound, southEastBound)));	
			float desiredZoomlevel = (float) (getZoomLevel (minimumLatitudeInGoogle,maximumLatitudeInGoogle,minimumLongitudeInGoogle,maximumLongitudeInGoogle,mapViewOutlet.Frame.Size.Width,mapViewOutlet.Frame.Size.Height));

			desiredZoomlevel-=1;
			mapView.MoveCamera (CameraUpdate.ZoomToZoom(desiredZoomlevel));
		}

		public override void ObserveValue (NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			//base.ObserveValue (keyPath, ofObject, change, context);

			if (!firstLocationUpdate) {
				// If the first location update has not yet been recieved, then jump to that
				// location.
				firstLocationUpdate = true;
				var location = change.ObjectForKey (NSValue.ChangeNewKey) as CLLocation;
				//mapView.Camera = CameraPosition.FromCamera (location.Coordinate, 14);
			}
		}

		public override void LoadView ()
		{
			base.LoadView ();
			CameraPosition camera = CameraPosition.FromCamera (37.797865, -122.402526,0);
			mapView = Google.Maps.MapView.FromCamera (RectangleF.Empty, camera);

			mapView.AddObserver (this, new NSString ("myLocation"), NSKeyValueObservingOptions.New, IntPtr.Zero); 


		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			cameraAutoZoomAndReposition (this.markersToAdd);
			loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			this.View.Add (loadingOverlay);


		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);



		}

		public override void ViewDidAppear (bool animated)
		{


			base.ViewDidAppear (animated);

			mapView.MyLocationEnabled = true;
			addMarkerAtLocationsWithGoogleMarker (this.markersToAdd);
			View = mapView;
			if (pathMarkers.Length > 1) {
				//Stuff from ViewWillAppear
				//Get path for the trip that will be shown on the map
				List<CLLocationCoordinate2D> googlePathCoordinates = new List<CLLocationCoordinate2D> ();
				foreach (CLLocationCoordinate2D coord in pathMarkers) {
					googlePathCoordinates.Add (coord);
				}

				GoogleMapsDirectionService gmds = new GoogleMapsDirectionService ();
				//Load polylines from file if saved, otherwise try loading trip coordinates from file
				List<Polyline> polylinesToPlot = new List<Polyline>();
				List<string> pathsReadFromFile = googlePathStrings;
				if(pathsReadFromFile.Count != 0) {
					gmds.resetLastCoordinateInPath ();
					foreach(string singlePath in pathsReadFromFile){
						List<CLLocationCoordinate2D> singlePathCoordinates = gmds.DecodePolylinePoints (singlePath);
						polylinesToPlot.Add(gmds.constructPolyline(singlePathCoordinates));
					}
					gmds.resetLastCoordinateInPath ();
				}
				else {
					polylinesToPlot = gmds.performGoogleDirectionServiceApiCallout (googlePathCoordinates);
				}

				if (polylinesToPlot.Count == 0) {
					new UIAlertView ("Cannot Access Path Services", "Path of the trip cannot be shown", null, "OK", null).Show ();
					cameraAutoZoomAndReposition (this.markersToAdd);

				} else {
					var startMarker = new Marker () {
						Title = "Start",
						Position = polylinesToPlot[0].Path.CoordinateAtIndex(0),
						Icon = Google.Maps.Marker.MarkerImage(UIColor.Green),
						Map = mapView
					};
					foreach (Polyline line in polylinesToPlot) {
						line.StrokeWidth = 10;
						line.StrokeColor = UIColor.FromRGBA (0f, 25 / 255f, 1f, 0.5f);
						line.Map = this.mapView;
					}
					var endMarker = new Marker () {
						Title = "End",
						Position = polylinesToPlot[polylinesToPlot.Count-1].Path.CoordinateAtIndex(polylinesToPlot[polylinesToPlot.Count-1].Path.Count-1),
						Icon = Google.Maps.Marker.MarkerImage(UIColor.Red),
						Map = mapView
					};
					cameraAutoZoomAndReposition (this.markersToAdd, polylinesToPlot.ToArray());

				}
			}
			mapView.StartRendering ();
			loadingOverlay.Hide ();
		}

		public override void ViewWillDisappear (bool animated)
		{	
			mapView.StopRendering ();
			base.ViewWillDisappear (animated);
		}
	}
}