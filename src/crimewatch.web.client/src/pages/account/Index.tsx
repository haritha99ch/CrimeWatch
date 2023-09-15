
const Index = () => {
    const content : JSX.Element = <>
    <div className="h-[80px]"></div>
    <div className="mx-auto w-full p-0 md:p-4 md:mx-auto md:w-1/2 ">
        <div className="md:container md:mx-auto container mx-auto">
            <div className="px-4 sm:px-0">
                <h3 className="text-base font-semibold leading-7 text-gray-900">Account Information</h3>
                <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500">Personal details.</p>
            </div>
            <div className="mt-6 border-t border-gray-100">
                <dl className="divide-y divide-gray-100">
                    <div className="description-list-group">
                        <dt className="description">Full name</dt>
                        <dd className="description-value">Margot Foster</dd>
                    </div>
                    <div className="description-list-group">
                        <dt className="description">Gender</dt>
                        <dd className="description-value">Male</dd>
                    </div>
                    <div className="description-list-group">
                        <dt className="description">Age</dt>
                        <dd className="description-value">23</dd>
                    </div>
                    <div className="description-list-group">
                        <dt className="description">Date Of Birth</dt>
                        <dd className="description-value">1999/06/18</dd>
                    </div>
                    <div className="description-list-group">
                        <dt className="description">Email</dt>
                        <dd className="description-value">haritha.cr@outlook.com</dd>
                    </div>
                    <div className="description-list-group">
                        <dt className="description">Phone Number</dt>
                        <dd className="description-value">070 5924 764</dd>
                    </div>
                </dl>
            </div>
        </div>
    </div>
    </>
  return content
}
export default Index