namespace SampleApp.Contracts.Data
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Header",  Namespace = "http://www.xyz.com/sampleapp/2012/02/Services/Data")]
    [System.SerializableAttribute()]
    public partial class Header : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject
            extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CultureInfoField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AppUserIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PartyIdField;

        //[System.Runtime.Serialization.OptionalFieldAttribute()]
        //private int IDField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RetailerCodeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TokenField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get { return this.extensionDataField; }
            set { this.extensionDataField = value; }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CultureInfo
        {
            get { return this.CultureInfoField; }
            set
            {
                if ((object.ReferenceEquals(this.CultureInfoField, value) != true))
                {
                    this.CultureInfoField = value;
                    this.RaisePropertyChanged("CultureInfo");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AppUserId
        {
            get { return this.AppUserIdField; }
            set
            {
                this.AppUserIdField = value;
                this.RaisePropertyChanged("AppUserId");
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PartyId
        {
            get { return this.PartyIdField; }
            set
            {
                this.PartyIdField = value;
                this.RaisePropertyChanged("PartyId");
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get { return this.UserNameField; }
            set
            {
                if ((object.ReferenceEquals(this.UserNameField, value) != true))
                {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RetailerCode
        {
            get { return this.RetailerCodeField; }
            set
            {
                if ((object.ReferenceEquals(this.RetailerCodeField, value) != true))
                {
                    this.RetailerCodeField = value;
                    this.RaisePropertyChanged("RetailerCode");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token
        {
            get { return this.TokenField; }
            set
            {
                if ((object.ReferenceEquals(this.TokenField, value) != true))
                {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}